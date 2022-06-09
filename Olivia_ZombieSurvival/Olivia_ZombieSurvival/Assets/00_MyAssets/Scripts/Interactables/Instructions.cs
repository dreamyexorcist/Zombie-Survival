using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject instructionsUI;
   
  
    void Start()
    {
        instructionsUI.SetActive(false);      
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            instructionsUI.SetActive(true);
            StartCoroutine("WaitForSec");
        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);        
        Destroy(gameObject);
    }

}
