using Player;
using UnityEngine;

namespace Invaders
{
    public class InvadersBullet : MonoBehaviour
    {
        private const float Speed = 5f;
        private Rigidbody2D _bullet;
        private const string Target = "Player";

        public void Start()
        {
            _bullet = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _bullet.position += Vector2.down * ( Speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.gameObject.CompareTag($"Invaders")) return;
            if (other.gameObject.CompareTag(Target))
            {
                var playerScript = other.GetComponent<PlayerScript>();
                playerScript.Kill();
                Destroy(gameObject);
            }
            if (other.CompareTag($"Bullet")) Destroy(gameObject);
        }
    }
}