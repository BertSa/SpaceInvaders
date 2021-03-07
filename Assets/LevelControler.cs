using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}