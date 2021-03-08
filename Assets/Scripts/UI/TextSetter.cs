using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (GameOver.IsInitialized)
                wlTitle.text = GameOver.Instance.GetState() == GameOver.WlState.Win ? "You Win!!!!" : "You Lost...";
            if (!ScoreManager.IsInitialized) return;
            scoreValue.text = ScoreManager.Instance.PlayerPoints.ToString();
            squidKilled.text = ScoreManager.Instance.SquidsKilled.ToString();
            crabKilled.text = ScoreManager.Instance.CrabsKilled.ToString();
            octopusKilled.text = ScoreManager.Instance.OctopusKilled.ToString();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("NRE:" + e);
        }
    }
}