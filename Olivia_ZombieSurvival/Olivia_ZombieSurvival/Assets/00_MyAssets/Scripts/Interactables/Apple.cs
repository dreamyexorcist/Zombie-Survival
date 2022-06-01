using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private int addStamina = 15;
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
            if (fpsController.currentStamina <= 0)
            {
                fpsController.currentStamina += addStamina;

                myUI.UpdateHealth(fpsController.currentStamina);

                // Debug.Log(fpsController.currentHealth);
                Destroy(this.gameObject);
            }

        }


    }
}
