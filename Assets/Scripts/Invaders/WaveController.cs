using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    private static int _direction;
    private static bool _movingX;
    private float _min = 0;
    private float _max = 5;
    private float _speed = 5;
    private float _previousY;

    void Start()
    {
        _direction = -1;
        _previousY = transform.position.y;
        _movingX = true;
        var rand = Random.Range(_min, _max);
        Invoke(nameof(SelectForFire), rand);
    }

    void Update()
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
            if (Math.Abs(transform.position.y - _previousY - (-1)) < 0.1 && !_movingX)
            {
                _direction *= -1;
                _movingX = true;
            }
            else
            {
                t.position += Vector3.down * (Time.deltaTime * _speed);
            }
        }
    }

    private void SelectForFire()
    {
        var levelOfAnger = LevelControler.Instance.GetLevelOfAnger();
        switch (levelOfAnger)
        {
            case LevelControler.LevelOfAnger.NotReallyGoodForYou:
            {
                _max = 1;
                break;
            }
            case LevelControler.LevelOfAnger.Rage:
            {
                _max = 2;
                break;
            }
            case LevelControler.LevelOfAnger.Mehh:
            {
                _max = 3;
                break;
            }
            case LevelControler.LevelOfAnger.Normal:
            {
                _max = 4;
                break;
            }
            default:
            {
                _max = 5;
                break;
            }
        }

        var rand = Random.Range(_min, _max);
        if (transform.childCount <= 0) return;
        var randomCol = (int) Math.Floor(Random.Range(_min, transform.childCount - 1));
        var column = transform.GetChild(randomCol);
        if (column.childCount > 0)
        {
            column.GetChild(column.childCount - 1).gameObject.GetComponent<Invader>().Invoke(nameof(Invader.Fire), 0f);
        }

        Invoke(nameof(SelectForFire), rand);
    }

    public void OnChildrenCollisionEnter()
    {
        if (!_movingX) return;
        _movingX = false;
        _previousY = transform.position.y;
    }
}