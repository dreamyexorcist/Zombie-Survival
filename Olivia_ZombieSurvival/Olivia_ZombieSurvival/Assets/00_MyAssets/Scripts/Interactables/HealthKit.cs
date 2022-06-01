using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private int addHealth = 55;
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

                myUI.UpdateHealth(fpsController.currentHealth);

                // Debug.Log(fpsController.currentHealth);
                Destroy(this.gameObject);
            }

        }


    }
}
