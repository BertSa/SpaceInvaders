using System;
using DesignPatterns;
using Enums;
using Events;

public class ScoreManager : Singleton<ScoreManager>
{
    public int SquidsKilled { get; private set; }
    public int CrabsKilled { get; private set; }
    public int OctopusKilled { get; private set; }
    public int PlayerPoints => SquidsKilled * 10 + CrabsKilled * 20 + OctopusKilled * 30;

    public EventValuesForHud ValuesForHud { get; } = new();

    public ScoreManager()
    {
        Reset();
    }

    public void Reset()
    {
        SquidsKilled = 0;
        CrabsKilled = 0;
        OctopusKilled = 0;
        ValuesForHud?.Invoke(PlayerPoints);
    }

    public void AddPointsPerTypes(InvaderTypes type)
    {
        switch (type)
        {
            case InvaderTypes.Squid:
                SquidsKilled++;
                break;
            case InvaderTypes.Crab:
                CrabsKilled++;
                break;
            case InvaderTypes.Octopus:
                OctopusKilled++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        ValuesForHud.Invoke(PlayerPoints);
    }
}