using DesignPatterns;
using UnityEngine;
using static GameState;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject dummyCamera;
        [SerializeField] private GameObject levelCamera;

        private void Start()
        {
            GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
        }

        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (previousState == Pregame && currentState == Running || previousState == Pause && currentState == Running)
            {
                mainMenu.gameObject.SetActive(false);
                dummyCamera.gameObject.SetActive(false);
                pauseMenu.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                levelCamera.gameObject.SetActive(true);
                hud.gameObject.SetActive(true);
            }

            if (previousState == Running && currentState == Pause)
            {
                pauseMenu.gameObject.SetActive(true);
            }

            if (previousState == Running && currentState is Lost or Won)
            {
                gameOver.gameObject.SetActive(true);
            }

            if (previousState is Lost or Won && currentState == Pregame)
            {
                gameOver.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
                dummyCamera.gameObject.SetActive(true);
                levelCamera.gameObject.SetActive(false);
                hud.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            switch (GameManager.Instance.CurrentGameState)
            {
                case Pregame:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameManager.Instance.StartGame();
                    }

                    break;
                }
                case Running:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        GameManager.Instance.PauseGame();
                    }

                    break;
                }
                case Pause:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        GameManager.Instance.ResumeGame();
                    }

                    break;
                }
                case Lost or Won:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameManager.Instance.BackToMenu();
                    }

                    break;
                }
            }
        }
    }
}