﻿using UnityEngine;

namespace Invaders
{
    public class InvadersCount : MonoBehaviour
    {
        #region PrivateFields

        private int _enemyCount;
        private int _enemiesAtStart;

        #endregion

        #region PrivateMethodes

        private void Start()
        {
            Invoke(nameof(DelayedStart), 0.5f);
        }

        private void DelayedStart()
        {
            _enemyCount = GameObject.FindGameObjectsWithTag("Invaders").Length;
            _enemiesAtStart = GameObject.FindGameObjectsWithTag("Invaders").Length;
        }

        #endregion

        #region PublicMethodes

        public void MinusOneEnemy()
        {
            _enemyCount--;
            if (_enemyCount == 0)
            {
                GameManager.Instance.NextLevel();
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

        #endregion

        #region Enums

        public enum LevelOfAnger
        {
            Fun,
            Normal,
            Mehh,
            Rage,
            NotReallyGoodForYou
        }

        #endregion
    }
}