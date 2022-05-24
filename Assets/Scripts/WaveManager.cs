using Enums;
using Level;

public class WaveManager
{
    private static readonly InvaderWave[] InvaderWaves =
    {
        new() { NbOfColumn = 6, Invaders = new[] { InvaderTypes.Squid, InvaderTypes.Octopus, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, }, },
        new() { NbOfColumn = 7, Invaders = new[] { InvaderTypes.Squid, InvaderTypes.Octopus, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, }, },
        new() { NbOfColumn = 7, Invaders = new[] { InvaderTypes.Squid, InvaderTypes.Octopus, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, }, },
        new() { NbOfColumn = 7, Invaders = new[] { InvaderTypes.Squid, InvaderTypes.Octopus, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, InvaderTypes.Crab, }, },
    };
    public static WaveManager Instance { get; } = new();

    private int Index { get; set; }
    public InvaderWave Current => InvaderWaves[Index];

    public void Reset() => Index = 0;
    public bool Next() => ++Index < InvaderWaves.Length;
}