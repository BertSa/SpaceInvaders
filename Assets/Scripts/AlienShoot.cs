using UnityEngine;

public class AlienShoot : MonoBehaviour
{
    private float _min = 0;
    private float _max = 20;
    void Start()
    {
        // float rand = Random.Range (_min, _max);
        // Invoke (nameof(SelectForFire), rand);
    }

    void Update()
    {
        
    }
    
    public void SelectForFire () {
        if (transform.childCount > 0) {
            
            transform.GetChild (transform.childCount-1).gameObject.GetComponent<AlienControler> ().Invoke (nameof(AlienControler.Fire), 0f);

            // float rand = Random.Range (_min, _max);
            // Invoke (nameof(SelectForFire), rand);
        } 
    }
}
