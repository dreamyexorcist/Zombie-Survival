using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float addHealth = 0.1f;
    private UI myUI;

    private void Start()
    {
        myUI = FindObjectOfType<UI>();
    }

    void OnTriggerEnter(Collider other)
    {

        FirstPersonController fpsController = other.GetComponent<FirstPersonController>();
        if (fpsController != null)
        {
            if (fpsController.currentHealth >= 0)
            {
                fpsController.currentHealth += addHealth;

                fpsController.currentHealth = fpsController.maxHealth;

                myUI.UpdateHealth(fpsController.currentHealth);

                // Debug.Log(fpsController.currentHealth);
                Destroy(this.gameObject);
            }

        }


    }
}
