using System;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class EventGameState : UnityEvent<GameState, GameState>
{
}

public class GameManager : Singleton<GameManager>
{
    public EventGameState onGameStateChanged;

    private readonly List<GameObject> _instanceSystemPrefabs = new();
    private readonly List<AsyncOperation> _loadOperations = new();
    private int IndexScene { get; set; }

    private readonly string[] _listScene =
    {
        "Level01",
        "Level02",
        "Level03",
    };

    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    public void Start()
    {
        DontDestroyOnLoad(this);
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

    private void LoadLevel(string levelName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (loadSceneAsync == null)
        {
            Debug.Log($"error loading scene : {levelName}");
            return;
        }

        loadSceneAsync.completed += OnLoadSceneComplete;
        _loadOperations.Add(loadSceneAsync);
    }

    private void UnloadLevel(string levelName, Action<AsyncOperation> onUnloadComplete)
    {
        var unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
        if (unloadSceneAsync == null)
        {
            Debug.Log($"error unloading scene : {levelName}");
            return;
        }

        unloadSceneAsync.completed += onUnloadComplete;
    }


    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (!_loadOperations.Contains(ao))
        {
            return;
        }

        _loadOperations.Remove(ao);

        if (_loadOperations.Count == 0)
        {
            UpdateGameState(GameState.Running);
        }
    }

    public void UpdateGameState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;
        onGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }

    public void StartGame()
    {
        LoadLevel(_listScene[IndexScene]);
    }

    public void PauseGame()
    {
        UpdateGameState(GameState.Pause);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        UpdateGameState(GameState.Running);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        UpdateGameState(GameState.Pregame);
        UnloadLevel(_listScene[IndexScene], _ => Debug.Log("unload completed"));
        Reset();
    }

    public void ResetGame()
    {
        UnloadLevel(_listScene[IndexScene], _ =>
        {
            Reset();
            StartGame();
            UpdateGameState(GameState.Running);
        });
    }

    public void Reset()
    {
        IndexScene = 0;
        LifeManager.Instance.Reset();
        ScoreManager.Instance.Reset();
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        if (GameState.Running != CurrentGameState)
        {
            return;
        }

        if (IndexScene >= _listScene.Length - 1)
        {
            Time.timeScale = 0;
            UpdateGameState(GameState.Won);
            return;
        }

        UnloadLevel(_listScene[IndexScene], _ =>
        {
            IndexScene++;
            LoadLevel(_listScene[IndexScene]);
        });
    }
}

public enum GameState
{
    Pregame,
    Running,
    Pause,
    Won,
    Lost,
}