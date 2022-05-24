using DesignPatterns;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private PlayerController playerController;
        private PlayerController _instantiatedPlayerController;

        private void Start()
        {
            Time.timeScale = 1;
        }

        protected override void Awake()
        {
            base.Awake();
            SpawnNewPlayer();
        }

        public void SpawnNewPlayer()
        {
            _instantiatedPlayerController = Instantiate(playerController, spawn.transform);
            _instantiatedPlayerController.levelManager = this;
        }
    }
}