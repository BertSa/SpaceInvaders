using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject dummyCamera;


    public void Start()
    {
        GameManager8.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager8.GameState currentState, GameManager8.GameState previousState)
    {
        if (previousState == GameManager8.GameState.Pregame && currentState == GameManager8.GameState.Running ||
            previousState == GameManager8.GameState.Pause && currentState == GameManager8.GameState.Running)
        {
            mainMenu.gameObject.SetActive(false);
            dummyCamera.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(false);
        }

        if (previousState == GameManager8.GameState.Running && currentState == GameManager8.GameState.Pause)
        {
            pauseMenu.gameObject.SetActive(true);
        }

        if (previousState == GameManager8.GameState.Running && currentState == GameManager8.GameState.Ending)
        {
            gameOver.gameObject.SetActive(true);
        }

        if (previousState == GameManager8.GameState.Ending && currentState == GameManager8.GameState.Pregame)
        {
            gameOver.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            dummyCamera.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.Pregame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager8.Instance.StartGame();
            }
        }
        else if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.Running)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager8.Instance.PauseGame();
            }
        }
        else if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager8.Instance.ResumeGame();
            }
        }
        else if (GameManager8.Instance.CurrentGameState == GameManager8.GameState.Ending)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager8.Instance.UnloadActualLevel();
            }
        }
    }
}