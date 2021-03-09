using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextSetter : MonoBehaviour
    {
        public Text wlTitle;
        public Text scoreValue;
        public Text squidKilled;
        public Text crabKilled;
        public Text octopusKilled;

        private void Update()
        {
            try
            {
                if (!ScoreManager.IsInitialized || !GameOver.IsInitialized) return;
                
                wlTitle.text = GameOver.Instance.GetState() == GameOver.WlState.Win ? "You Win!!!!" : "You Lost...";
                scoreValue.text = ScoreManager.Instance.GetPlayerPoints().ToString();
                squidKilled.text = ScoreManager.Instance.GetSquidKilled().ToString();
                crabKilled.text = ScoreManager.Instance.GetCrabKilled().ToString();
                octopusKilled.text = ScoreManager.Instance.GetOctopusKilled().ToString();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("NRE:" + e);
            }
        }
    }
}