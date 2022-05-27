using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    int selectedWeapon = 0;

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }
    }
}
