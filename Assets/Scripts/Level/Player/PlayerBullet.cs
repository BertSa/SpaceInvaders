using Level.Invaders;
using UnityEngine;

namespace Level.Player
{
    public class PlayerBullet : Bullet
    {
        private const string Target = "Invaders";

        public PlayerBullet() : base(20f, Vector2.up)
        {
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject.CompareTag(Target))
            {
                var component = other.GetComponent<Invader>();
                component.Kill();
                Destroy(gameObject);
            }
        }
    }
}