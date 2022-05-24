using DesignPatterns;
using Enums;
using UnityEngine;

public class GameOver : Singleton<GameOver>
{
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState currentState, GameState previousState)
    {
        if (currentState is not (GameState.Lost or GameState.Won))
        {
            return;
        }

        if (!ScoreManager.IsInitialized)
        {
            Debug.Log("ScoreManager Not Initialized!");
            return;
        }

        var data = Save.LoadFile();
        if (data != null && data.Score < ScoreManager.Instance.PlayerPoints)
        {
            Save.SaveFile(ScoreManager.Instance.PlayerPoints, "Sam");
        }
    }
}