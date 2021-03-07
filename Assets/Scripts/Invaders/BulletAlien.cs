using UnityEngine;

public class BulletAlien : BulletScript
{
    public BulletAlien()
    {
        Target = "Player";
        Direction = -1;
        _speed = 5f;
    }

    protected new void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.gameObject.CompareTag($"Invaders"))
        {
            base.OnTriggerEnter2D(other);
        }
    }
}