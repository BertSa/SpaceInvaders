using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using DesignPatterns;
using UnityEngine;

public class GameOver : Singleton<GameOver>
{
    private WlState _state;
    private bool _gameOver;

    public void SetOverWithWinner(WlState state)
    {
        _state = state;
        _gameOver = true;
        SaveFile();
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

    private void SaveFile()
    {
        var destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

        var data = new GameData(ScoreManager.Instance.PlayerPoints, "Sam");
        var bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    #region Enums

    public enum WlState
    {
        Win,
        Lost
    }

    #endregion
}