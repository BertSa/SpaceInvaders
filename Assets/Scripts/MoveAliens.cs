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
        var levelOfAnger = LevelControler.Instance.GetLevelOfAnger();
        switch (levelOfAnger)
        {
            case LevelControler.LevelOfAnger.NotReallyGoodForYou:
                _speed = 10;
                break;
            case LevelControler.LevelOfAnger.Rage:
                _speed = 5;
                break;
            case LevelControler.LevelOfAnger.Mehh:
                _speed = 4;
                break;
            case LevelControler.LevelOfAnger.Normal:
                _speed = 3;
                break;
            default:
                _speed = 1;
                break;
        }

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
        if (!LevelControler.IsInitialized) return;
        LevelControler.Instance.EnemyCount--;
    }
}