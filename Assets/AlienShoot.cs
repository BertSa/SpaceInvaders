using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShoot : MonoBehaviour
{
    public float min = 0;
    public float max = 20;
    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range (min, max);
        Invoke (nameof(SelectForFire), rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SelectForFire () {
        if (transform.childCount > 0) {
            
            transform.GetChild (transform.childCount-1).gameObject.GetComponent<AlienControler> ().Invoke ("Fire", 0f);

            float rand = Random.Range (min, max);
            Invoke ("SelectForFire", rand);
        } 
    }
}
