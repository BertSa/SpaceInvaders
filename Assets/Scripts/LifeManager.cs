using DesignPatterns;

public class LifeManager : Singleton<LifeManager>
{
    private const int DefaultAmountOfLives = 3;
    private int _lives;

    public EventValuesForHud eventValuesForHud;

    public void OnPlayerKilled()
    {
        if (GameManager.Instance.CurrentGameState != GameState.Running) return;
        _lives--;
        eventValuesForHud.Invoke(_lives);
        if (_lives <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Lost);
        }
    }

    public void Reset()
    {
        _lives = DefaultAmountOfLives;
        eventValuesForHud.Invoke(_lives);
    }

    private void Start()
    {
        Reset();
    }
}