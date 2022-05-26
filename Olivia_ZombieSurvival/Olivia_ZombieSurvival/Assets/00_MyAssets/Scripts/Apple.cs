using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private bool eatApple = true;

    private CharacterController characterController;
    private int currentHealth;

    private int apple = 15;
    private Object GameObject;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (currentHealth <= 0)
                currentHealth += (currentHealth + apple);
        }

        Destroy(this.GameObject);
    }
}
