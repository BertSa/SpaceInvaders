using DesignPatterns;
using Level.Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private PlayerController playerController;
        private PlayerController _instantiatedPlayerController;

        protected override void Awake()
        {
            base.Awake();
            SpawnNewPlayer();
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        public void SpawnNewPlayer()
        {
            _instantiatedPlayerController = Instantiate(playerController, spawn.transform);
            _instantiatedPlayerController.levelManager = this;
        }
    }
}