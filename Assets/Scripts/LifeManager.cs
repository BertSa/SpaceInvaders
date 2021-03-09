public class LifeManager : Singleton<LifeManager>
{
    private static readonly int DefaultAmountOfLives = 3;
    private int _lives;

    void Start()
    {
        _lives = DefaultAmountOfLives;
    }

    public void OnPlayerKilled()
    {
        _lives--;
        if (_lives == 0)
        {
            GameOver.Instance.SetOver(GameOver.WlState.Lost);
        }
    }

    public void Reset()
    {
        _lives = DefaultAmountOfLives;
    }
}