using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;

    public AudioSource[] clips;
    private bool keyInput = true;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }
    void Update()
    {
        keyInput = Input.GetKeyDown(KeyCode.E);

    }

    void OnTriggerStay(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();

        if (other.CompareTag("Player") && keyInput)
            if (fpsController != null)
            {
                clips[0].Play();
                clips[1].Play();
            }
    }

}

