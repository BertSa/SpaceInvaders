using DesignPatterns;

public class GameOver : Singleton<GameOver>
{
    private WlState _state;
    private bool _gameOver;

    public void SetOverWithWinner(WlState state)
    {
        _state = state;
        _gameOver = true;
        GameManager.Instance.GameIsOver();
    }

    public WlState GetState()
    {
        return _state;
    }

    public void Reset()
    {
        _gameOver = false;
    }

    #region Enums

    public enum WlState
    {
        Win,
        Lost
    }

    #endregion
}