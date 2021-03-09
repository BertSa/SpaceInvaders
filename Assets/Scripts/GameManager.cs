using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
{
}

// Donne au GameManager le controle sur l'ordre de 'loading' des sous-systemes.
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
    // public GameObject[] systemPrefabs;

    private string _currentLevelName = string.Empty;
    private readonly List<GameObject> _instanceSystemPrefabs = new List<GameObject>();

    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    private int _indexScene;
    private readonly string[] _listScene = {"Level01", "Level02"};

    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    public void Start()
    {
        DontDestroyOnLoad(this);
        // InstantiateSystemPrefab();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_instanceSystemPrefabs != null)
        {
            foreach (var prefabInstance in _instanceSystemPrefabs) Destroy(prefabInstance);

            _instanceSystemPrefabs.Clear();
        }
    }

    // private void InstantiateSystemPrefab()
    // {
    //     for (var i = 0; i < systemPrefabs.Length; i++)
    //     {
    //         var prefabInstance = Instantiate(systemPrefabs[i]);
    //         _instanceSystemPrefabs.Add(prefabInstance);
    //     }
    // }

    private void LoadLevel(string levelName)
    {
        _currentLevelName = levelName;

        var loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (loadSceneAsync == null) // La scene existe dans le build setting
        {
            print("error loading scene : " + levelName);
            return;
        }

        loadSceneAsync.completed += OnLoadSceneComplete;
        _loadOperations.Add(loadSceneAsync);
    }

    private void UnloadLevel(string levelName)
    {
        var unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
        if (unloadSceneAsync == null)
        {
            print("error unloading scene : " + levelName);
            return;
        }

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
        Invoke(nameof(Reset), 0.5f);
    }

    public void ResetGame()
    {
        UnloadLevel(_listScene[_indexScene]);
        Invoke(nameof(Restart), 0.1f);
        _indexScene = 0;
        StartGame();
        Invoke(nameof(Yeds), 0.1f);
    }


    private void Yeds()
    {
        UpdateGameState(GameState.Running);
    }

    private void Restart()
    {
        Reset();
    }

    public void Reset()
    {
        _indexScene = 0;
        LifeManager.Instance.Reset();
        GameOver.Instance.Reset();
        ScoreManager.Instance.Reset();
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

        Time.timeScale = 0;
        if (_indexScene >= _listScene.Length - 1)
        {
            GameOver.Instance.SetOver(GameOver.WlState.Win);
        }
        else
        {
            UnloadLevel(_listScene[_indexScene]);
            _indexScene++;
            LoadLevel(_listScene[_indexScene]);
        }
    }
}