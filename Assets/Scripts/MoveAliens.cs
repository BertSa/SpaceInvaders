using System;
using UnityEngine;

public class MoveAliens : MonoBehaviour
{
    private static int _direction;
    private static bool _movingX;
    private float _previousY;
    private Rigidbody2D _alien;
    private float _speed = 5;

    public void Start()
    {
        _alien = GetComponent<Rigidbody2D>();
        _direction = -1;
        _previousY = transform.position.y;
        _movingX = true;
    }

    public void Update()
    {
        var instanceEnemiesAtStart = (double) LevelControler.Instance.EnemiesAtStart / 100;
        var count = LevelControler.Instance.EnemyCount;
        if (count < (instanceEnemiesAtStart * 5))
            _speed = 10;
        else if (count < (instanceEnemiesAtStart * 10))
            _speed = 5;
        else if (count < (instanceEnemiesAtStart * 25))
            _speed = 4;
        else if (count < (instanceEnemiesAtStart * 50))
            _speed = 3;
        else if (count < (instanceEnemiesAtStart * 75))
            _speed = 2;
        else
            _speed = 1;

        var t = transform;
        if (_movingX)
        {
            t.position += Vector3.right * (_direction * Time.deltaTime * _speed);
        }
        else
        {
            t.position += Vector3.down * (Time.deltaTime * _speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag($"Wall")) return;
        _movingX = false;
        _previousY = transform.position.y;
    }

    // ReSharper disable once UnusedParameter.Local
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!(Math.Abs(transform.position.y - _previousY - (-1)) < 0.1) || _movingX) return;
        _direction *= -1;
        _movingX = true;
        _alien.velocity = Vector2.right * _speed * _direction;
    }

    private void OnDestroy()
    {
        LevelControler.Instance.EnemyCount--;
    }
}