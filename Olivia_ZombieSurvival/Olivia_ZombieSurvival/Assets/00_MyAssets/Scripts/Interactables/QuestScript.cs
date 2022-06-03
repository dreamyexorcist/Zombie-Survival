using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    [SerializeField] private GameObject quest1UI;
    [SerializeField] private GameObject quest2UI;
    [SerializeField] private GameObject quest3UI;
    [SerializeField] private GameObject quest4UI;

    bool nearQuest = false;

    [SerializeField] private bool questOneDone = false;
    [SerializeField] private bool questTwoDone = false;
    [SerializeField] private bool questThreeDone = false;
    [SerializeField] private bool questFourDone = false;

    private bool finished = true;
    private bool unfinished = false;

    void Start()
    {
       //quest2UI.SetActive(false);
       //quest3UI.SetActive(false);
       //quest4UI.SetActive(false);
    }
    void Update()
    {
        if (nearQuest && Input.GetKeyDown(KeyCode.E))
        {
            Quest1();
                  
        }
        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questTwoDone = unfinished))
        {           
            Quest2();            
        }
        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questThreeDone = unfinished))
        {
            Quest3();
        }
        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questFourDone = unfinished))
        {
            Quest4();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearQuest = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearQuest = false;
        }
    }

    private void Quest1()
    {
        quest1UI.SetActive(false);

        if (gameObject.CompareTag("Quest1"))
        {
            questOneDone = true;
        }
        if (questOneDone = finished)
        {
            quest2UI.SetActive(true);
        }

    }
    private void Quest2()
    {
        quest2UI.SetActive(false);

        if (gameObject.CompareTag("Quest2"))
        {
            questTwoDone = true;
        }
        if (questTwoDone = finished)
        {
            quest3UI.SetActive(true);
        }

        /*
        if (gameObject.CompareTag("Quest2"))
        {
            questTwoDone = true;
        }*/
    }
    private void Quest3()
    {
        quest3UI.SetActive(false);

        if (gameObject.CompareTag("Quest3"))
        {
            questThreeDone = true;
        }
        if (questThreeDone = finished)
        {
            quest4UI.SetActive(true);
        }
        
         if (gameObject.CompareTag("Quest3"))
         {
             questThreeDone = true;
         }
    }
    private void Quest4()
    {
        quest4UI.SetActive(false);

        if (gameObject.CompareTag("Quest4"))
        {
            questFourDone = true;
        }
    }
       

}

