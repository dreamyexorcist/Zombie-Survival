using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private int apple = 15;
    //[SerializeField] UI myUI;

    private void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        FirstPersonController fpsController = other.GetComponent<FirstPersonController>();
        if (fpsController != null)
        {
            if (fpsController.currentHealth >= 0)
            {
                fpsController.currentHealth += apple;

                //myUI.UpdateHealth(fpsController.currentHealth);

                // Debug.Log(fpsController.currentHealth);
                Destroy(this.gameObject);
            }

        }


    }
}
