using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text scoreValue;
    [SerializeField] private Text livesValue;


    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.eventValuesForHud.AddListener(UpdateScoreValue);
        LifeManager.Instance.eventValuesForHud.AddListener(UpdateLivesValue);
    }

    private void UpdateLivesValue(int value)
    {
        livesValue.text = value.ToString();
    }

    private void UpdateScoreValue(int value)
    {
        scoreValue.text = value.ToString();
    }
}

[Serializable]
public class EventValuesForHud : UnityEvent<int>
{
}