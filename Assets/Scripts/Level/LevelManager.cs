using System;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject spawn;
        public PlayerScript player;
        private PlayerScript _playerScript;
        
        private void Start()
        {
            Time.timeScale = 1;
            SpawnNewPlayer();
        }

        public void SpawnNewPlayer()
        {
            _playerScript = Instantiate(player, spawn.transform);
            _playerScript.LevelManager = this;
        }

        private void OnDestroy()
        {
            // Destroy(spawn);
            // _playerScript.enabled = false;
            // var findGameObjectWithTag = GameObject.FindGameObjectWithTag($"MainCamera");
            // Destroy(findGameObjectWithTag);
        }
        
    }
}