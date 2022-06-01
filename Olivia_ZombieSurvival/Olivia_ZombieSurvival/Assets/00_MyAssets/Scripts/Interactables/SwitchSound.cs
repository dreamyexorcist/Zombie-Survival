using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    private AudioSource[] audioClips;
    private AudioSource generator;
    private AudioSource electricity;



    void Start()
    {
        audioClips = GetComponents<AudioSource>();
        generator = audioClips[0];
        electricity = audioClips[1];
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            generator.Play();
            electricity.Play();

        }
    }
}
