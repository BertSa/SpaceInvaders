using System;
using DesignPatterns;
using Invaders;
using static Invaders.InvaderTypes;

public class ScoreManager : Singleton<ScoreManager>
{
    public int PlayerPoints { get; private set; }
    public int SquidsKilled { get; private set; }
    public int CrabsKilled { get; private set; }
    public int OctopusKilled { get; private set; }

    public EventValuesForHud eventValuesForHud;

    public ScoreManager()
    {
        Reset();
    }


    public void AddPointsPerTypes(InvaderTypes type)
    {
        switch (type)
        {
            case Squid:
                PlayerPoints += 10;
                SquidsKilled++;
                break;
            case Crab:
                PlayerPoints += 20;
                CrabsKilled++;
                break;
            case Octopus:
                PlayerPoints += 30;
                OctopusKilled++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        eventValuesForHud.Invoke(PlayerPoints);
    }

    public void Reset()
    {
        PlayerPoints = 0;
        SquidsKilled = 0;
        CrabsKilled = 0;
        OctopusKilled = 0;
        eventValuesForHud?.Invoke(PlayerPoints);
    }
}