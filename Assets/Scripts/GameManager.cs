using System;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
{
}

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running,
        Pause,
        Ending
    }

    public EventGameState onGameStateChanged;

    private readonly List<GameObject> _instanceSystemPrefabs = new List<GameObject>();

    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    private int _indexScene;

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
        if (_instanceSystemPrefabs == null) return;
        foreach (var prefabInstance in _instanceSystemPrefabs) Destroy(prefabInstance);

        _instanceSystemPrefabs.Clear();
    }

    private void LoadLevel(string levelName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ReturnIfNull(loadSceneAsync, true, levelName)) return;

        loadSceneAsync.completed += OnLoadSceneComplete;
        _loadOperations.Add(loadSceneAsync);
    }

    private static bool ReturnIfNull(AsyncOperation loadSceneAsync, bool load, string levelName)
    {
        if (loadSceneAsync != null) return false;
        print((load) ? "error loading scene : " : "error unloading scene : " + levelName);
        return true;
    }

    private void UnloadLevel(string levelName)
    {
        var unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
        if (ReturnIfNull(unloadSceneAsync, false, levelName)) return;

        unloadSceneAsync.completed += OnUnloadSceneComplete;
    }


    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            // Ici on peut aviser les composantes qui ont besoin de savoir que le level est loadé
            if (_loadOperations.Count == 0) UpdateGameState(GameState.Running);
        }

        print("load completed");
    }

    private void OnUnloadSceneComplete(AsyncOperation obj)
    {
        print("unload completed");
    }

    private void UpdateGameState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;
        switch (CurrentGameState)
        {
            case GameState.Pregame:
                break;
            case GameState.Running:
                break;
            case GameState.Pause:
                break;
            case GameState.Ending:
                break;
        }

        onGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }

    public void StartGame()
    {
        LoadLevel(_listScene[_indexScene]);
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

    // ReSharper disable Unity.PerformanceAnalysis
    public void BackToMenu()
    {
        UpdateGameState(GameState.Pregame);
        UnloadLevel(_listScene[_indexScene]);
        Reset();
    }

    public void ResetGame()
    {
        var unloadSceneAsync = SceneManager.UnloadSceneAsync(_listScene[_indexScene]);
        if (ReturnIfNull(unloadSceneAsync, false, _listScene[_indexScene])) return;

        unloadSceneAsync.completed += OnUnloadSceneCompleteForRestart;
    }

    private void OnUnloadSceneCompleteForRestart(AsyncOperation obj)
    {
        Reset();
        StartGame();
        UpdateGameState(GameState.Running);
    }


    public void Reset()
    {
        _indexScene = 0;
        LifeManager.Instance.Reset();
        ScoreManager.Instance.Reset();
        Time.timeScale = 1;
    }

    public void GameIsOver()
    {
        UpdateGameState(GameState.Ending);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        if (GameState.Running != CurrentGameState)
            return;

        if (_indexScene >= _listScene.Length - 1)
        {
            Time.timeScale = 0;
            GameOver.Instance.SetOverWithWinner(GameOver.WlState.Win);
        }
        else
        {
            var unloadSceneAsync = SceneManager.UnloadSceneAsync(_listScene[_indexScene]);
            if (ReturnIfNull(unloadSceneAsync, false, _listScene[_indexScene])) return;

            unloadSceneAsync.completed += OnUnloadSceneCompleteForNextLevel;
        }
    }

    private void OnUnloadSceneCompleteForNextLevel(AsyncOperation obj)
    {
        _indexScene++;
        LoadLevel(_listScene[_indexScene]);
    }
}