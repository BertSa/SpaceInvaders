using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject spawn;
        public PlayerScript player;
        private PlayerScript _instantiatedPlayer;

        private void Start()
        {
            Time.timeScale = 1;
            SpawnNewPlayer();
        }

//TODO animation when player respawn
        public void SpawnNewPlayer()
        {
            _instantiatedPlayer = Instantiate(player, spawn.transform);
            _instantiatedPlayer.LevelManager = this;
        }
    }
}