using Player;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private GameObject spawn;
        [SerializeField] private Player.Player player;

        #endregion

        #region PrivateFields

        private Player.Player _instantiatedPlayer;

        #endregion

        #region PrivateMethods

        private void Start()
        {
            Time.timeScale = 1;
            SpawnNewPlayer();
        }

        #endregion

        #region PublicMethods

        //TODO animation when player respawn
        public void SpawnNewPlayer()
        {
            _instantiatedPlayer = Instantiate(player, spawn.transform);
            _instantiatedPlayer.levelManager = this;
        }

        #endregion
    }
}