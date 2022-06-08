using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private float addStamina = 1f;
    [SerializeField] private FirstPersonController fpsController;
    private UI staminaUI;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        staminaUI = FindObjectOfType<UI>();
    }

    void OnTriggerEnter(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>();
        staminaUI.GetComponent<UI>();

        if ((fpsController != null) && (fpsController.currentStamina >= 0))
        {
            fpsController.currentStamina += addStamina;
            staminaUI.UpdateStamina(fpsController.currentStamina += addStamina);
            Destroy(this.gameObject);

        }        
    }
}
