using UnityEngine;

public class LevelControler : Singleton<LevelControler>
{
    private int _enemyCount;
    private int _enemiesAtStart;

    public LevelControler()
    {
        _enemyCount = 0;
        _enemiesAtStart = 0;
    }

    private void Start()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Alien").Length;
        _enemiesAtStart = GameObject.FindGameObjectsWithTag("Alien").Length;
    }

    private void Update()
    {
    }

    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }

    public int EnemiesAtStart => _enemiesAtStart;


    public LevelOfAnger GetLevelOfAnger()
    {
        var instanceEnemiesAtStart = (double) EnemiesAtStart / 100;
        if (EnemyCount < (instanceEnemiesAtStart * 10))
            return LevelOfAnger.NotReallyGoodForYou;
        if (EnemyCount < (instanceEnemiesAtStart * 25))
            return LevelOfAnger.Rage;
        if (EnemyCount < (instanceEnemiesAtStart * 50))
            return LevelOfAnger.Mehh;
        if (EnemyCount < (instanceEnemiesAtStart * 75))
            return LevelOfAnger.Normal;
        return LevelOfAnger.Fun;
    }

    public enum LevelOfAnger
    {
        Fun,
        Normal,
        Mehh,
        Rage,
        NotReallyGoodForYou
    }
}