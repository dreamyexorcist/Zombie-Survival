using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
   public GameObject[] lights;

    bool nearPowerBox = false;
    bool lightsOn = false;

    public AudioClip generator;
    public AudioClip lightSound;

    
    private AudioSource audioSource;

    private void Start()
    {
       // lightSwitch = GetComponent<Light>();

        
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (nearPowerBox && Input.GetKeyDown(KeyCode.E))
        {
            TurnOnLights();

            //lightSwitch.enabled = !lightSwitch.enabled;
            audioSource.clip = lightSound;
            audioSource.Play();

        }
        else if(lightsOn)
        {
            audioSource.clip = generator;
            audioSource.Play();
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

}
