using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private Text wlTitle;
        [SerializeField] private Text highScore;
        [SerializeField] private Text scoreValue;
        [SerializeField] private Text squidKilled;
        [SerializeField] private Text crabKilled;
        [SerializeField] private Text octopusKilled;

        private int _currentScore;
        private string _currentName;


        private void OnEnable()
        {
            if (!ScoreManager.IsInitialized || !GameOver.IsInitialized)
            {
                return;
            }

            var gameData = Save.LoadFile();

            if (gameData == null)
            {
                Debug.Log("Error: No save");
                return;
            }

            _currentName = gameData.Name;
            _currentScore = gameData.Score;

            wlTitle.text = GameManager.Instance.CurrentGameState == GameState.Won ? "You Win!!!!" : "You Lost...";
            scoreValue.text = "Your Score\n" + ScoreManager.Instance.PlayerPoints;
            squidKilled.text = ScoreManager.Instance.SquidsKilled.ToString();
            crabKilled.text = ScoreManager.Instance.CrabsKilled.ToString();
            octopusKilled.text = ScoreManager.Instance.OctopusKilled.ToString();
            highScore.text = "HighScore :" + _currentName + "(" + _currentScore + ")";
        }
    }
}