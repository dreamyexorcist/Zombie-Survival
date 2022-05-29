using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private Light lightSwitch;
    private AudioSource[] audioClips;
    private AudioSource generator;
    private AudioSource electricity;

    private void Start()
    {
        lightSwitch = GetComponent<Light>();

        audioClips = GetComponents<AudioSource>();
        generator = audioClips[0];
        electricity = audioClips[1];
        
      //audioClips = gameObject.GetComponent<AudioSource>(); 
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            lightSwitch.enabled = !lightSwitch.enabled;
            //audioClip.enabled = !audioClip.enabled;
            generator.Play();
            electricity.Play();
                      
        }
    }
}
