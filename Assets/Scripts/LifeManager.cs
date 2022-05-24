using DesignPatterns;
using Enums;
using Events;
using UnityEngine;

public class LifeManager : Singleton<LifeManager>
{
    private const int DefaultAmountOfLives = 3;
    private int _lives;

    public EventValuesForHud ValuesForHud { get; } = new();

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _lives = DefaultAmountOfLives;
        ValuesForHud.Invoke(_lives);
    }

    public void OnPlayerKilled()
    {
        if (GameManager.Instance.CurrentGameState != GameState.Running)
        {
            return;
        }

        _lives--;
        ValuesForHud.Invoke(_lives);
    }
}