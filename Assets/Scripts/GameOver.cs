using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using DesignPatterns;
using UnityEngine;

public class GameOver : Singleton<GameOver>
{
    private WlState _state;

    public void SetOverWithWinner(WlState state)
    {
        _state = state;
        var data = Save.LoadFile();
        if (data.score < ScoreManager.Instance.PlayerPoints)
        {
            Save.SaveFile(ScoreManager.Instance.PlayerPoints, "Sam");
        }

        GameManager.Instance.GameIsOver();
    }

    public WlState GetState()
    {
        return _state;
    }

    #region Enums

    public enum WlState
    {
        Win,
        Lost
    }

    #endregion
}