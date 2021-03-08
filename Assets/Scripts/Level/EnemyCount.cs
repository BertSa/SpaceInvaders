using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;

public class EnemyCount : Singleton<EnemyCount>
{
    private int _enemyCount;
    private int _enemiesAtStart;

    void Start()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Invaders").Length;
        _enemiesAtStart = GameObject.FindGameObjectsWithTag("Invaders").Length;
    }

    void Update()
    {
        
    }

    public void MinusOneEnemy()
    {
        _enemyCount--;
        if (_enemyCount==0)
        {
            LevelControler.Instance.LevelCompleted();
        }
    }
    
    public LevelOfAnger GetLevelOfAnger()
    {
        var instanceEnemiesAtStart = (double) _enemiesAtStart / 100;
        if (_enemyCount < (instanceEnemiesAtStart * 10))
            return LevelOfAnger.NotReallyGoodForYou;
        if (_enemyCount < (instanceEnemiesAtStart * 25))
            return LevelOfAnger.Rage;
        if (_enemyCount < (instanceEnemiesAtStart * 50))
            return LevelOfAnger.Mehh;
        if (_enemyCount < (instanceEnemiesAtStart * 75))
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
