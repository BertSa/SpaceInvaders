using Level;
using UnityEngine;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] public GameObject bullet;
        private float _timeStamp;
        private const float Speed = 6;
        private const float CoolDownPeriodInSeconds = 0.2f;
        public LevelManager LevelManager { get; set; }


        public void Update()
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
//TODO animation when player killed

        public void Kill()
        {
            if (LifeManager.IsInitialized)
            {
                LifeManager.Instance.OnPlayerKilled();
                LevelManager.SpawnNewPlayer();
            }

            Destroy(gameObject);
        }
    }
}