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
            LoadFile();
            
            wlTitle.text = GameOver.Instance.GetState() == GameOver.WlState.Win ? "You Win!!!!" : "You Lost...";
            scoreValue.text = "Your Score\n"+ScoreManager.Instance.PlayerPoints;
            squidKilled.text = ScoreManager.Instance.SquidsKilled.ToString();
            crabKilled.text = ScoreManager.Instance.CrabsKilled.ToString();
            octopusKilled.text = ScoreManager.Instance.OctopusKilled.ToString();
            highScore.text = "HighScore :" + _currentName + "(" + _currentScore + ")";

        }

        private void LoadFile()
        {
            var destination = Application.persistentDataPath + "/save.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            var data = (GameData) bf.Deserialize(file);
            file.Close();

            _currentScore = data.score;
            _currentName = data.name;
        }
    }
}