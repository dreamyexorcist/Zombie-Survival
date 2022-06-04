using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float weaponRange = 10f;
    [SerializeField] float weaponDamage = 50f;
    [SerializeField] GameObject weaponHitEffect;

      
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    
    }

    private void Fire()
    {
        RaycastHit objectHit; //Store info about the object that is hit. 

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out objectHit, weaponRange))
        {
            CreateHitEffect(objectHit);
           // Debug.Log(objectHit.transform.name);
            EnemyHealth enemyHealthScript = objectHit.transform.GetComponent<EnemyHealth>();

            if(enemyHealthScript == null) { return; }
               enemyHealthScript.TakeDamage(weaponDamage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitEffect(RaycastHit objectHit)
    {
        GameObject bulletHit = Instantiate(weaponHitEffect, objectHit.point, Quaternion.identity);
        Destroy(bulletHit, 2f);
    }
  
}
