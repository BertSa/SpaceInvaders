using UnityEngine;

public class BulletScript : MonoBehaviour
{
    protected float _speed = 10f;
    private Rigidbody2D _bullet;
    protected float Direction;
    protected string Target;

    public void Start()
    {
        _bullet = GetComponent<Rigidbody2D>();
        _bullet.velocity = new Vector2(0, Direction * _speed);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Target))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}