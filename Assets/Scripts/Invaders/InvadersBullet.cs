using Player;
using UnityEngine;

namespace Invaders
{
    public class InvadersBullet : MonoBehaviour
    {
        #region PrivateFields

        private const float Speed = 5f;
        private const string Target = "Player";
        private Rigidbody2D _bullet;

        #endregion

        #region PrivateMethodes

        private void Start()
        {
            _bullet = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _bullet.position += Vector2.down * (Speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag($"Invaders")) return;
            if (other.gameObject.CompareTag(Target))
            {
                var playerScript = other.GetComponent<Player.Player>();
                playerScript.Kill();
                Destroy(gameObject);
            }

            if (other.CompareTag($"Bullet")) Destroy(gameObject);
        }

        #endregion
    }
}