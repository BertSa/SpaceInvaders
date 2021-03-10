using Level;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private GameObject bullet;
        [SerializeField] private AudioClip clip;

        #endregion


        #region PublicFields

        public LevelManager levelManager;

        #endregion

        #region PrivateFields

        private float _timeStamp;
        private const float Speed = 6;
        private const float CoolDownPeriodInSeconds = 0.5f;

        #endregion

        #region PublicMethods

        //TODO animation when player killed
        public void Kill()
        {
            if (LifeManager.IsInitialized)
            {
                LifeManager.Instance.OnPlayerKilled();
                levelManager.SpawnNewPlayer();
            }
            var source = GameManager.Instance.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            Destroy(source, 1);
            Destroy(gameObject);
        }

        #endregion

        #region PrivateMethods
        

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            transform.position += Vector3.right * horizontal;
            if (Input.GetKey("space") && (_timeStamp <= Time.time))
            {
                _timeStamp = Time.time + CoolDownPeriodInSeconds;
                var position = transform.position;
                var x = position.x;
                var y = position.y + 1;

                Instantiate(bullet, new Vector3(x, y), Quaternion.identity);
            }
        }

        #endregion
    }
}