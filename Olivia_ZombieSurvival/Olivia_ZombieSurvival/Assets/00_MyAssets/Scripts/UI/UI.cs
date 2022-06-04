using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;
    [SerializeField] private TextMeshProUGUI staminaText = default;
    FirstPersonController firstPersonController;


   
    private void OnEnable()
    {
        FirstPersonController.OnDamage += UpdateHealth; //update health UI when onDamage is called.
        FirstPersonController.OnHeal += UpdateHealth;
        FirstPersonController.OnStaminaChange += UpdateStamina;
    }

    private void OnDisable()
    {
        FirstPersonController.OnDamage -= UpdateHealth; //end updating health / reference.
        FirstPersonController.OnHeal -= UpdateHealth;
        FirstPersonController.OnStaminaChange -= UpdateStamina;
    }

    private void Start()
    {
        firstPersonController = FindObjectOfType<FirstPersonController>();

        UpdateHealth(firstPersonController.currentHealth); //default health
        UpdateStamina(firstPersonController.currentStamina);

    }



    public void UpdateHealth(float currentHealth) //value displayed on screen
    {
        healthText.text = currentHealth.ToString("00");        
    }

    public void UpdateStamina(float currentStamina)
    {
        staminaText.text = currentStamina.ToString("00");       
    }
    public void loadOpeningScene()
    {
        SceneManager.LoadScene(1);
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void loadSettingsScene()
    {
        SceneManager.LoadScene(4);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
