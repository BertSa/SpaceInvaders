using Level.Player;
using UnityEngine;

namespace Level.Invaders
{
    public class InvadersBullet : Bullet
    {
        private const string Target = "Player";

        public InvadersBullet() : base(5f, Vector2.down)
        {
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject.CompareTag("Invaders"))
            {
                return;
            }

            if (other.gameObject.CompareTag(Target))
            {
                var playerScript = other.GetComponent<PlayerController>();
                playerScript.Kill();
                Destroy(gameObject);
            }
        }
    }
}