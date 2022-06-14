using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatEventSound : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    public AudioSource[] clips;    

   /* private void Start()
    {
       // fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }    */

    void OnTriggerStay(Collider other)
    {
        //fpsController.GetComponent<FirstPersonController>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            clips[0].Play();
            clips[1].Play();
        }
        else { return; }
    }

}
