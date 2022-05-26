using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour

{
    [SerializeField] float fireDamage = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BurnThePlayer()
    {
        GetComponent<PlayerHealth>().Damage(fireDamage);
    }
}
