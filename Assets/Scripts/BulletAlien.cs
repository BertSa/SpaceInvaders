using UnityEngine;

public class BulletAlien : BulletScript
{
    public BulletAlien()
    {
        Target = "Player";
        Direction = -1;
        _speed = 5f;
    }

    protected new void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag($"Alien"))
        {
            base.OnCollisionEnter2D(other);
        }
    }
}