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
    public GameObject[] systemPrefabs;

    private string _currentLevelName = string.Empty;
    private readonly List<GameObject> _instanceSystemPrefabs = new List<GameObject>();

    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    private int indexScene;
    private readonly string[] listScene = {"SampleScene", "sas"};

    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;

    public void Start()
    {
        DontDestroyOnLoad(this);
        InstanciateSystemPrefab();
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

    private void InstanciateSystemPrefab()
    {
        GameObject prefabInstance;
        for (var i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            _instanceSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
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

    public void UnloadLevel(string levelName)
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
            if (_loadOperations.Count == 0) UpdateGameState(GameState.RUNNING);
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
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                break;
            case GameState.PAUSE:
                break;
        }

        OnGameStateChanged.Invoke(CurrentGameState, previousGameState);
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