using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private int apple = 15;
    UI ui;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        FirstPersonController fpsController = other.GetComponent<FirstPersonController>();
        if (fpsController != null)
        {
            if (fpsController.currentHealth >= 0)
            {
                fpsController.currentHealth += apple;

                ui.UpdateHealth(fpsController.currentHealth);

                // Debug.Log(fpsController.currentHealth);
                Destroy(this.gameObject);
            }

        }


    }
}
