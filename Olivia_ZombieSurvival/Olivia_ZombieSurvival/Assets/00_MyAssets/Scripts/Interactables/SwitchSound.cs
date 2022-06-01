using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{

    bool nearPowerBox = false;
  //public BoxCollider power;

    private AudioSource[] audioClips;
    private AudioSource generator;
    private AudioSource electricity;



    void Start()
    {
      //power = GetComponent<BoxCollider>(); //need check for nearPowerBox collision
        audioClips = GetComponents<AudioSource>();
        
        generator = audioClips[0];
        electricity = audioClips[1];
    }
   
    void Update()
    {
        
        if (nearPowerBox && Input.GetKeyDown(KeyCode.E))
        {
            generator.Play();
            electricity.Play();

        }
    }
}
