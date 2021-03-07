using System;
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
    }

    private void Update()
    {
        _bullet.position += Vector2.up * (Direction * _speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Target))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}