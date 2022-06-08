using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueEventSound : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;

    public AudioSource[] clips;
    private bool available = true;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        available = true;
    }

    void OnTriggerStay(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && available && fpsController != null)
        {
            clips[0].Play();
            clips[1].Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        available = false;
    }

}

