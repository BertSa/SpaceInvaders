using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject dummyCamera;

    public void Start()
    {
        GameManager8.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager8.GameState currentState, GameManager8.GameState previousState)
    {
        if (previousState == GameManager8.GameState.PREGAME && currentState == GameManager8.GameState.RUNNING ||
            previousState == GameManager8.GameState.PAUSE && currentState == GameManager8.GameState.RUNNING)
        {
            mainMenu.gameObject.SetActive(false);
            dummyCamera.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
        }

        if (previousState == GameManager8.GameState.RUNNING && currentState == GameManager8.GameState.PAUSE)
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.PREGAME)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager8.Instance.StartGame();
            }
        }
        else if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.RUNNING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager8.Instance.PauseGame();
            }
        }
        else if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.PAUSE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager8.Instance.ResumeGame();
            }
        }
    }
}