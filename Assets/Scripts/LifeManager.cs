using DesignPatterns;

public class LifeManager : Singleton<LifeManager>
{
    private const int DefaultAmountOfLives = 3;
    private int _lives;

    private void Start()
    {
        _lives = DefaultAmountOfLives;
    }

    public void OnPlayerKilled()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Running) return;
        _lives--;
        if (_lives <= 0)
        {
            GameOver.Instance.SetOverWithWinner(GameOver.WlState.Lost);
        }
    }

    public void Reset()
    {
        _lives = DefaultAmountOfLives;
    }
}