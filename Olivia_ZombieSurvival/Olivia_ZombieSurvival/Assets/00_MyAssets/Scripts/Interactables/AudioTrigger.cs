using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    public AudioSource[] playSound;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();

        if (other.CompareTag("Player"))

            if (fpsController != null)
            {
                playSound[0].Play();
                playSound[1].Play();
            }

    }
}
