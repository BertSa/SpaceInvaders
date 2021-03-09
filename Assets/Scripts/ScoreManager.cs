using Invaders;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int _playerPoints;
    private int _squidsKilled;
    private int _crabsKilled;
    private int _octopusKilled;

    public ScoreManager()
    {
        Reset();
    }


    public void AddPointsPerTypes(Invader.InvaderTypes type)
    {
        switch (type)
        {
            case Invader.InvaderTypes.Squid:
                _playerPoints += 10;
                _squidsKilled++;
                break;
            case Invader.InvaderTypes.Crab:
                _playerPoints += 20;
                _crabsKilled++;
                break;
            case Invader.InvaderTypes.Octopus:
                _playerPoints += 30;
                _octopusKilled++;
                break;
        }
        print(_playerPoints);
    }

    public void Reset()
    {
        _playerPoints = 0;
        _squidsKilled = 0;
        _crabsKilled = 0;
        _octopusKilled = 0;
    }

    public int GetPlayerPoints()
    {
        return _playerPoints;
    }

    public int GetSquidKilled()
    {
        return _squidsKilled;
    }

    public int GetCrabKilled()
    {
        return _crabsKilled;
    }

    public int GetOctopusKilled()
    {
        return _octopusKilled;
    }
}