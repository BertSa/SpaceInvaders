using Level;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private const float Speed = 6;
        private const float CoolDownPeriodInSeconds = 0.5f;

        [SerializeField] private GameObject bullet;
        [SerializeField] private AudioClip clip;

        public LevelManager levelManager;

        private float _timeStamp;

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

        private void Update()
        {
            Move();

            Fire();
        }

        private void Move()
        {
            var movement = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;

            transform.position += Vector3.right * movement;
        }

        private void Fire()
        {
            if (!Input.GetKey("space"))
            {
                return;
            }

            if (_timeStamp > Time.time)
            {
                return;
            }

            _timeStamp = Time.time + CoolDownPeriodInSeconds;

            var position = transform.position;
            var x = position.x;
            var y = position.y + 1;

            Instantiate(bullet, new Vector3(x, y), Quaternion.identity);
        }
    }
}