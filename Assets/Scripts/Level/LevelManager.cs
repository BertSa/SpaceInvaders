using System;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private Player.Player player;

        private Player.Player _instantiatedPlayer;

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void Awake()
        {
            SpawnNewPlayer();
        }

        public void SpawnNewPlayer()
        {
            _instantiatedPlayer = Instantiate(player, spawn.transform);
            _instantiatedPlayer.levelManager = this;
        }
    }
}