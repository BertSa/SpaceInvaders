using System;
using System.Collections.Generic;
using DesignPatterns;
using Enums;
using Events;
using Invaders;
using Level;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private const string LevelSceneName = "LevelScene";

    private readonly List<GameObject> _instanceSystemPrefabs = new();
    private readonly List<AsyncOperation> _loadOperations = new();

    public EventGameState OnGameStateChanged { get; } = new();
    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    public void Start()
    {
        DontDestroyOnLoad(this);
        UIManager.Instance.eventUserInteraction.AddListener(HandleInteraction);
        LifeManager.Instance.ValuesForHud.AddListener(lives =>
        {
            if (lives <= decimal.Zero)
            {
                UpdateGameState(GameState.Lost);
            }
        });
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (_instanceSystemPrefabs == null)
        {
            return;
        }

        foreach (var prefabInstance in _instanceSystemPrefabs)
        {
            Destroy(prefabInstance);
        }

        _instanceSystemPrefabs.Clear();
    }

    private void LoadLevel()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(LevelSceneName, LoadSceneMode.Additive);
        if (loadSceneAsync == null)
        {
            Debug.Log($"error loading scene : {LevelSceneName}");
            return;
        }

        loadSceneAsync.completed += operation =>
        {
            if (!_loadOperations.Contains(operation))
            {
                return;
            }

            _loadOperations.Remove(operation);

            if (_loadOperations.Count != 0)
            {
                return;
            }

            UpdateGameState(GameState.Running);
            InvadersManager.Instance.triggered.AddListener(NextLevel);
            BottomLine.Instance.triggered.AddListener(() => UpdateGameState(GameState.Lost));
        };
        _loadOperations.Add(loadSceneAsync);
    }

    private void UnloadLevel(Action<AsyncOperation> onUnloadComplete)
    {
        BottomLine.Instance.triggered.RemoveAllListeners();
        InvadersManager.Instance.triggered.RemoveAllListeners();

        var unloadSceneAsync = SceneManager.UnloadSceneAsync(LevelSceneName);
        if (unloadSceneAsync == null)
        {
            Debug.Log($"error unloading scene : {LevelSceneName}");
            return;
        }

        unloadSceneAsync.completed += onUnloadComplete;
    }

    private void HandleInteraction(UserInteraction interaction)
    {
        switch (interaction)
        {
            case UserInteraction.Start:
                LoadLevel();
                break;
            case UserInteraction.Pause:
                UpdateGameState(GameState.Pause);
                Time.timeScale = 0;
                break;
            case UserInteraction.Resume:
                UpdateGameState(GameState.Running);
                Time.timeScale = 1;
                break;
            case UserInteraction.BackToMenu:
                UpdateGameState(GameState.Pregame);
                UnloadLevel(_ => Debug.Log("unload completed"));
                Reset();
                break;
        }
    }

    private void UpdateGameState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;
        OnGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }

    private void Reset()
    {
        WaveManager.Instance.Reset();
        LifeManager.Instance.Reset();
        ScoreManager.Instance.Reset();
        Time.timeScale = 1;
    }

    private void NextLevel()
    {
        if (GameState.Running != CurrentGameState)
        {
            return;
        }

        if (!WaveManager.Instance.Next())
        {
            Time.timeScale = 0;
            UpdateGameState(GameState.Won);
            return;
        }

        UnloadLevel(_ => LoadLevel());
    }
}