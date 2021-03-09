using DesignPatterns;
using UnityEngine;
using static GameManager.GameState;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        #region SerializedFields

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject dummyCamera;
        [SerializeField] private GameObject levelCamera;

        #endregion


        private void Start()
        {
            GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
        }

        //TODO game ui
        private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            if (previousState == Pregame && currentState == Running ||
                previousState == Pause && currentState == Running)
            {
                mainMenu.gameObject.SetActive(false);
                dummyCamera.gameObject.SetActive(false);
                pauseMenu.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                levelCamera.gameObject.SetActive(true);
            }

            if (previousState == Running && currentState == Pause)
            {
                pauseMenu.gameObject.SetActive(true);
            }

            if (previousState == Running && currentState == Ending)
            {
                gameOver.gameObject.SetActive(true);
            }

            if (previousState == Ending && currentState == Pregame)
            {
                gameOver.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
                dummyCamera.gameObject.SetActive(true);
                levelCamera.gameObject.SetActive(false);
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
                case Ending:
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