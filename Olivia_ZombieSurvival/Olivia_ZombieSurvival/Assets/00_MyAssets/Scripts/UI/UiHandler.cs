using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
    public void loadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void loadCinematicScene()
    {
        SceneManager.LoadScene(1);
    }
    public void loadLevel1Scene()  
    {
        SceneManager.LoadScene(2);  
    }
    public void loadLevel2Scene()
    {
        SceneManager.LoadScene(3);
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
