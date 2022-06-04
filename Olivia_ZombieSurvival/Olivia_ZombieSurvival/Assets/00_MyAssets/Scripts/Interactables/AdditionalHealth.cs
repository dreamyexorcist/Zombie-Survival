using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalHealth : MonoBehaviour
{

    [SerializeField] private float additionalHealth = 30f;
    [SerializeField] private FirstPersonController fpsController;
    private UI myUI;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        myUI = FindObjectOfType<UI>();
    }

    void OnTriggerStay(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))

            if (fpsController != null)
            {
                fpsController.maxHealth = fpsController.maxHealth + additionalHealth;
                myUI.UpdateHealth(fpsController.maxHealth);
                Destroy(this.gameObject);
            }
    }
}

