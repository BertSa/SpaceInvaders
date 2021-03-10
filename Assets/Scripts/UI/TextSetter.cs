using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextSetter : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private Text wlTitle;
        [SerializeField] private Text highScore;
        [SerializeField] private Text scoreValue;
        [SerializeField] private Text squidKilled;
        [SerializeField] private Text crabKilled;
        [SerializeField] private Text octopusKilled;

        #endregion

        private int _currentScore;
        private string _currentName;


        private void OnEnable()
        {
            if (!ScoreManager.IsInitialized || !GameOver.IsInitialized) return;
            var gameData = Save.LoadFile();
            _currentName = gameData.name;
            _currentScore = gameData.score;

            wlTitle.text = GameOver.Instance.GetState() == GameOver.WlState.Win ? "You Win!!!!" : "You Lost...";
            scoreValue.text = "Your Score\n" + ScoreManager.Instance.PlayerPoints;
            squidKilled.text = ScoreManager.Instance.SquidsKilled.ToString();
            crabKilled.text = ScoreManager.Instance.CrabsKilled.ToString();
            octopusKilled.text = ScoreManager.Instance.OctopusKilled.ToString();
            highScore.text = "HighScore :" + _currentName + "(" + _currentScore + ")";
        }
    }
}