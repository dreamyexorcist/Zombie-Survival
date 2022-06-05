using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private float addStamina = 1f;
    [SerializeField] private FirstPersonController fpsController;
    private UI myUI;

    private void Start()
    {
        fpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        myUI = FindObjectOfType<UI>();
    }

    void OnTriggerEnter(Collider other)
    {
        myUI.GetComponent<UI>();

        if (fpsController != null)
        {
            if (fpsController.currentStamina >= 0)
            {
                fpsController.currentStamina += addStamina;
                myUI.UpdateStamina(fpsController.currentStamina += addStamina);              
                Destroy(this.gameObject);
            }
        }
    }
}
