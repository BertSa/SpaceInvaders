public class ScoreManager : Singleton<ScoreManager>
{
    public int PlayerPoints { get; private set; }
    public int SquidsKilled { get; private set; }
    public int CrabsKilled { get; private set; }
    public int OctopusKilled { get; private set; }

    public ScoreManager()
    {
        Reset();
    }


    public void AddPointsPerTypes(Invader.InvaderTypes type)
    {
        switch (type)
        {
            case Invader.InvaderTypes.Squid:
                PlayerPoints += 10;
                SquidsKilled++;
                break;
            case Invader.InvaderTypes.Crab:
                PlayerPoints += 20;
                CrabsKilled++;
                break;
            case Invader.InvaderTypes.Octopus:
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
}