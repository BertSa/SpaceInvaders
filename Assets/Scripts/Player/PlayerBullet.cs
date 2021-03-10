using Invaders;
using UnityEngine;

namespace Player
{
    public class PlayerBullet : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private AudioClip clip;

        #endregion

        #region PrivateFields

        private readonly float _speed;
        private readonly string _target;
        private Rigidbody2D _bullet;

        #endregion

        public PlayerBullet()
        {
            _speed = 20f;
            _target = "Invaders";
        }

        #region PrivateMethods

        public void Start()
        {
            _bullet = GetComponent<Rigidbody2D>();
            var addComponent = GameManager.Instance.gameObject.AddComponent<AudioSource>();
            addComponent.clip = clip;
            addComponent.Play();
            Destroy(addComponent, 1);
        }

        private void Update()
        {
            _bullet.position += Vector2.up * (_speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_target))
            {
                var component = other.GetComponent<Invader>();
                component.Kill();
                Destroy(gameObject);
            }

            if (other.CompareTag($"Bullet"))
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}