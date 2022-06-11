using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    public GameObject gunType;
    public GameObject weapons;
    [SerializeField] public bool weaponCollected;
    
    private void Start()
    {
        //fpsController = GetComponent<FirstPersonController>();        
        gunType.SetActive(true);
        weaponCollected = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //fpsController.GetComponent<Weapon>();

        if (other.gameObject.tag == "Player")
        {
            gunType.SetActive(false);
            weapons.SetActive(true);
            weaponCollected = true;
        }        

    }
}
