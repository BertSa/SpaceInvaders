using UnityEngine;

namespace Level
{
    public class LevelControler : Singleton<LevelControler>
    {
        public GameObject spawn;
        public PlayerScript player;

        private void Start()
        {
            Time.timeScale = 1;
            SpawnNewPlayer();
        }

        public void OnEnemyKill(Invader.InvaderTypes type)
        {
            if (ScoreManager.IsInitialized && EnemyCount.IsInitialized)
            {
                ScoreManager.Instance.AddPointsPerTypes(type);
                EnemyCount.Instance.MinusOneEnemy();
            }
        }
        

        public void LevelCompleted()
        {
            GameManager.Instance.NextLevel();
        }

        public void SpawnNewPlayer()
        {
            Instantiate(player, spawn.transform);
        }
    }
}