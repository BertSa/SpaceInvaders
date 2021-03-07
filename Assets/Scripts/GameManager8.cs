using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class EventGameState : UnityEvent<GameManager8.GameState, GameManager8.GameState>
{
}

// Donne au GameManager le controle sur l'ordre de 'loading' des sous-systemes.
public class GameManager8 : Singleton<GameManager8>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE
    }

    public EventGameState OnGameStateChanged;

    private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    public GameObject[] systemPrefabs;
    private List<GameObject> _instanceSystemPrefabs = new List<GameObject>();
    private GameState _currentGameState = GameState.PREGAME;
    private String[] listScene = {"SampleScene", "sas"};
    private int indexScene = 0;

    private string _currentLevelName = string.Empty;

    public void Start()
    {
        DontDestroyOnLoad(this);
        InstanciateSystemPrefab();
    }

    void InstanciateSystemPrefab()
    {
        GameObject prefabInstance;
        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            _instanceSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_instanceSystemPrefabs != null)
        {
            foreach (var prefabInstance in _instanceSystemPrefabs)
            {
                Destroy(prefabInstance);
            }

            _instanceSystemPrefabs.Clear();
        }
    }

    public void LoadLevel(string levelName)
    {
        _currentLevelName = levelName;

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (loadSceneAsync == null) // La scene existe dans le build setting
        {
            print("error loading scene : " + levelName);
            return;
        }

        loadSceneAsync.completed += OnLoadSceneComplete;
        _loadOperations.Add(loadSceneAsync);
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
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
            if (_loadOperations.Count == 0)
            {
                UpdateGameState(GameState.RUNNING);
            }
        }

        print("load completed");
    }

    private void OnUnloadSceneComplete(AsyncOperation obj)
    {
        print("unload completed");
    }

    void UpdateGameState(GameState newGameState)
    {
        var previousGameState = _currentGameState;
        _currentGameState = newGameState;
        switch (_currentGameState)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                break;
            case GameState.PAUSE:
                break;
            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    public GameState CurrentGameState
    {
        get => _currentGameState;
        private set => _currentGameState = value;
    }

    public void StartGame()
    {
        LoadLevel(listScene[indexScene]);
    }

    public void PauseGame()
    {
        UpdateGameState(GameState.PAUSE);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        UpdateGameState(GameState.RUNNING);
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        UnloadLevel(listScene[indexScene]);
        indexScene = 0;
        LoadLevel(listScene[indexScene]);
        Time.timeScale = 1;
    }
}