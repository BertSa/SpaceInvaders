public class GameOver : Singleton<GameOver>
{
    private WlState _state;
    private bool _gameOver;

    public void SetOver(WlState state)
    {
        _state = state;
        _gameOver = true;
        GameManager8.Instance.GameIsOver();
    }

    public enum WlState
    {
        Win,
        Lost
    }

    public WlState GetState()
    {
        return _state;
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }
}