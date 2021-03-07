using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControler : MonoBehaviour
{
    [SerializeField] public GameObject bullet;
    private float _timeStamp;
    private const float CoolDownPeriodInSeconds = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Fire()
    {
        var position = transform.position;
        var x = position.x;
        var y = position.y - 1;

        if (_timeStamp <= Time.time)
        {
            _timeStamp = Time.time + CoolDownPeriodInSeconds;
            Instantiate(bullet, new Vector3(x, y, 5), Quaternion.identity);
        }
    }
}