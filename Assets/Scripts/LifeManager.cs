using DesignPatterns;

public class LifeManager : Singleton<LifeManager>
{
    private const int DefaultAmountOfLives = 3;
    private int _lives;
    
    public EventValuesForHud eventValuesForHud;

    #region PublicMethods

    public void OnPlayerKilled()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Running) return;
        _lives--;
        eventValuesForHud.Invoke(_lives);
        if (_lives <= 0)
        {
            GameOver.Instance.SetOverWithWinner(GameOver.WlState.Lost);
        }
    }

    public void Reset()
    {
        _lives = DefaultAmountOfLives;
        eventValuesForHud.Invoke(_lives);
    }

    #endregion

    #region PrivateMethods

    private void Start()
    {
        Reset();
    }

    #endregion

    
}