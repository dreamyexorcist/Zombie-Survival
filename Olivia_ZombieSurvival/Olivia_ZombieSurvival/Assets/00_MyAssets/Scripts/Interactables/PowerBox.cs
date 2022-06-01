using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    public GameObject[] lights;

    bool nearPowerBox = false;
    bool lightsOn = false;
    private Light lightSwitch;


    //private AudioSource audioSource;

    private void Start()
    {
         lightSwitch = GetComponent<Light>();

    }

    void Update()
    {
        if (nearPowerBox && Input.GetKeyDown(KeyCode.E))
        {
            TurnOnLights();
            
        }

        if (nearPowerBox && Input.GetKeyDown(KeyCode.E))
        {
            TurnOffLight();
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
        lightsOn = true;
        foreach (GameObject light in lights)
        {
            Light lightComponentOnEachLight = light.GetComponent<Light>();
            if (lightComponentOnEachLight != null)
            {
                lightComponentOnEachLight.enabled = true;

            }

        }

    }

    private void TurnOffLight()
    {
        lightsOn = false;
        foreach (GameObject light in lights)
        {
            Light lightComponentOnEachLight = light.GetComponent<Light>();
            if (lightComponentOnEachLight = null)
            {
                lightComponentOnEachLight.enabled = false;

            }

        }

    }

}
