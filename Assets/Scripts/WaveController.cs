using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    private float _min = 0;
    private float _max = 5;

    void Start()
    {
        float rand = Random.Range(_min, _max);
        Invoke(nameof(SelectForFire), rand);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SelectForFire()
    {
        var levelOfAnger = LevelControler.Instance.GetLevelOfAnger();
        switch (levelOfAnger)
        {
            case LevelControler.LevelOfAnger.NotReallyGoodForYou:
                _max = 1;
                break;
            case LevelControler.LevelOfAnger.Rage:
                _max = 2;
                break;
            case LevelControler.LevelOfAnger.Mehh:
                _max = 3;
                break;
            case LevelControler.LevelOfAnger.Normal:
                _max = 4;
                break;
            default:
                _max = 5;
                break;
        }

        if (transform.childCount > 0)
        {
            print("oui");
            int randomChild = (int) Math.Floor(Random.Range(_min, transform.childCount - 1));
            transform.GetChild(randomChild).gameObject.GetComponent<AlienShoot>()
                .Invoke(nameof(AlienShoot.SelectForFire), 0f);

            float rand = Random.Range(_min, _max);
            Invoke(nameof(SelectForFire), rand);
        }
    }
}