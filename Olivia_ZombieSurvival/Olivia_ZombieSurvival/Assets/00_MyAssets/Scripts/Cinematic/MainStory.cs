using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStory : MonoBehaviour
{
    void OnEnable()
    {
        //Only specifying the sceneName or sceneBuildIndex will load the Scene with the single mode
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
