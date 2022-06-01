using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    //[SerializeField] Camera weaponCamera;
    [SerializeField] int selectedWeapon = 0;

   /* private void Start()
    {
        weaponCamera = GetComponent<Camera>();
    }*/


    void Update()
    {
        ActiveWeapon();
        SelectWeapon();
    }

    private void ActiveWeapon()
    {
        int weaponIndex = 0;
        foreach(Transform weapon in transform)
        {
            if(selectedWeapon == weaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
            
        }
    }

    private void SelectWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        
    }
}
