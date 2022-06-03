using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImpactDamage : MonoBehaviour
{
    [SerializeField] private float dmg = 1f;
    //public GameObject EnemyHealth;

    private void Start()
    {
        GetComponent<FirstPersonController>();
        //GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonController.OnTakeDamage(dmg);
            //EnemyHealth.enemyHealth(dmg);
        }         
    }

    private void OnTriggerExit(Collider other)
    {
        return;
    }
}
