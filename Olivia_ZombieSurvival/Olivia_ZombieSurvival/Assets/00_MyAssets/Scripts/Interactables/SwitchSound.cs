using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    public AudioSource generator;
    public AudioSource electricity;

    [SerializeField] private FirstPersonController fpsController;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    void OnTriggerStay(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            if (fpsController != null)
            {
                generator.Play();
                electricity.Play();
            }
    }
}

