using DesignPatterns;
using Enums;
using Events;
using UnityEngine;
using static Enums.GameState;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        public EventUserInteraction eventUserInteraction;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject dummyCamera;
        [SerializeField] private GameObject levelCamera;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        private void Update()
        {
            var spaceDown = Input.GetKeyDown(KeyCode.Space);
            var escapeDown = Input.GetKeyDown(KeyCode.Escape);

            switch (GameManager.Instance.CurrentGameState)
            {
                case Pregame when spaceDown:
                    eventUserInteraction.Invoke(UserInteraction.Start);
                    break;
                case Running when escapeDown:
                    eventUserInteraction.Invoke(UserInteraction.Pause);
                    break;
                case Pause when escapeDown:
                    eventUserInteraction.Invoke(UserInteraction.Resume);
                    break;
                case Lost or Won when spaceDown:
                    eventUserInteraction.Invoke(UserInteraction.BackToMenu);
                    break;
            }
        }

        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            switch (previousState, currentState)
            {
                case (Pregame or Pause, Running):
                    mainMenu.gameObject.SetActive(false);
                    dummyCamera.gameObject.SetActive(false);
                    pauseMenu.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(false);
                    levelCamera.gameObject.SetActive(true);
                    hud.gameObject.SetActive(true);
                    break;
                case (Running, Pause):
                    pauseMenu.gameObject.SetActive(true);
                    break;
                case (Running, Lost or Won):
                    gameOver.gameObject.SetActive(true);
                    break;
                case (Lost or Won, Pregame):
                    gameOver.gameObject.SetActive(false);
                    mainMenu.gameObject.SetActive(true);
                    dummyCamera.gameObject.SetActive(true);
                    levelCamera.gameObject.SetActive(false);
                    hud.gameObject.SetActive(false);
                    break;
            }
        }
    }
}