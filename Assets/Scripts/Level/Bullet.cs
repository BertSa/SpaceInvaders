using UnityEngine;

namespace Level
{
    public abstract class Bullet : MonoBehaviour
    {
        private readonly float _speed;
        private readonly Vector2 _direction;

        private Rigidbody2D _bullet;

        [SerializeField] private AudioClip clip;

        protected Bullet(float speed, Vector2 direction)
        {
            _speed = speed;
            _direction = direction;
        }

        private void Start()
        {
            _bullet = GetComponent<Rigidbody2D>();
            var source = GameManager.Instance.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            Destroy(source, 1);
        }

        private void Update()
        {
            _bullet.position += _direction * (_speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}