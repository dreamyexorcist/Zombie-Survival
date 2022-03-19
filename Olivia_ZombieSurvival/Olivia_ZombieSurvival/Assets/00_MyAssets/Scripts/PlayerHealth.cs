using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] Canvas playerDeadCanvas;

    private void Start()
    {
        playerDeadCanvas.enabled = false;
    }

    public void Damage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        playerDeadCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; //don't confine to any window
        Cursor.visible = true; //show the cursor
    }
}
