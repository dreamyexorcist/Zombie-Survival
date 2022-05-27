using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private bool eatApple = true;

    private int apple = 15;
    private Object GameObject;

    private void Start()
    {
            
    }

    void OnTriggerEnter(Collider other)
    {
        FirstPersonController fpsController = other.GetComponent<FirstPersonController>();
        if (fpsController != null)
        {
            if (other.tag == "Player")
            {
                if (fpsController.currentHealth >= 0)
                    fpsController.currentHealth += (fpsController.currentHealth + apple);
            }

            Destroy(this.GameObject);
        }
        
    }
}
