using DesignPatterns;
using Invaders;
using static Invaders.Invader.InvaderTypes;

public class ScoreManager : Singleton<ScoreManager>
{
    #region EncapsulatedFields

    public int PlayerPoints { get; private set; }
    public int SquidsKilled { get; private set; }
    public int CrabsKilled { get; private set; }
    public int OctopusKilled { get; private set; }

    #endregion

    public ScoreManager()
    {
        Reset();
    }


    #region PublicMethods

    public void AddPointsPerTypes(Invader.InvaderTypes type)
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
        }
    }

    public void Reset()
    {
        PlayerPoints = 0;
        SquidsKilled = 0;
        CrabsKilled = 0;
        OctopusKilled = 0;
    }

    #endregion
}