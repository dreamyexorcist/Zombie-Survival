using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
    public void loadOpeningScene()  
    {

        SceneManager.LoadScene(1);  

    }

    public void QuitGame()
    {

        Application.Quit(); 

    }

}
