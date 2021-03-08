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
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.Pregame && currentState == GameManager.GameState.Running ||
            previousState == GameManager.GameState.Pause && currentState == GameManager.GameState.Running)
        {
            mainMenu.gameObject.SetActive(false);
            dummyCamera.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(false);
        }

        if (previousState == GameManager.GameState.Running && currentState == GameManager.GameState.Pause)
        {
            pauseMenu.gameObject.SetActive(true);
        }

        if (previousState == GameManager.GameState.Running && currentState == GameManager.GameState.Ending)
        {
            gameOver.gameObject.SetActive(true);
        }

        if (previousState == GameManager.GameState.Ending && currentState == GameManager.GameState.Pregame)
        {
            gameOver.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            dummyCamera.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.Pregame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.StartGame();
            }
        }
        else if (GameManager.Instance.CurrentGameState == GameManager.GameState.Running)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
            }
        }
        else if (GameManager.Instance.CurrentGameState == GameManager.GameState.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ResumeGame();
            }
        }
        else if (GameManager.Instance.CurrentGameState == GameManager.GameState.Ending)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.UnloadActualLevel();
            }
        }
    }
}