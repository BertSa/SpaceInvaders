using UnityEngine;

namespace Level.Player
{
    public class PlayerController : MonoBehaviour, IKillable
    {
        private const float Speed = 6;
        private const float CoolDownPeriodInSeconds = 0.5f;

        [SerializeField] private GameObject bullet;
        [SerializeField] private AudioClip clip;
        [HideInInspector] public LevelManager levelManager;

        private float _timeStamp;

        private void Update()
        {
            Move();

            Fire();
        }

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

        private void Move()
        {
            var movement = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;

            transform.position += Vector3.right * movement;
        }

        private void Fire()
        {
            if (!Input.GetKey(KeyCode.Space)) return;

            if (_timeStamp > Time.time) return;

            _timeStamp = Time.time + CoolDownPeriodInSeconds;

            var position = transform.position;

            var pos = new Vector3 { x = position.x, y = position.y + 1, };
            Instantiate(bullet, pos, Quaternion.identity);
        }
    }
}