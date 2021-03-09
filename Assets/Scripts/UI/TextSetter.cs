using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextSetter : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private Text wlTitle;
        [SerializeField] private Text scoreValue;
        [SerializeField] private Text squidKilled;
        [SerializeField] private Text crabKilled;
        [SerializeField] private Text octopusKilled;

        #endregion


        private void OnEnable()
        {
            if (!ScoreManager.IsInitialized || !GameOver.IsInitialized) return;

            wlTitle.text = GameOver.Instance.GetState() == GameOver.WlState.Win ? "You Win!!!!" : "You Lost...";
            scoreValue.text = ScoreManager.Instance.PlayerPoints.ToString();
            squidKilled.text = ScoreManager.Instance.SquidsKilled.ToString();
            crabKilled.text = ScoreManager.Instance.CrabsKilled.ToString();
            octopusKilled.text = ScoreManager.Instance.OctopusKilled.ToString();
        }
    }
}