using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    public GameObject[] lights;
    bool nearPowerBox = false;


    void Update()
    {
        if (nearPowerBox && Input.GetKeyDown(KeyCode.E))
        {
            TurnOnLights();
            
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearPowerBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearPowerBox = false;
        }
    }

    private void TurnOnLights()
    {
        foreach (GameObject light in lights)
        {
            Light lightComponentOnEachLight = light.GetComponent<Light>();
            if (lightComponentOnEachLight != null)
            {
                lightComponentOnEachLight.intensity = 20f;
            }

        }

    }

}
