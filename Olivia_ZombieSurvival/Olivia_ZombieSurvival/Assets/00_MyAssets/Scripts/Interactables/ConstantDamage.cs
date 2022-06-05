using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantDamage : MonoBehaviour
{
    //[SerializeField] public float dmg = 5f;
    //public FirstPersonController currentHealth;    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
             FirstPersonController.OnTakeDamage(0.05f);       
    }
}
