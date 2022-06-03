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

    [SerializeField] private bool questOne = false;
    [SerializeField] private bool questTwo = false;
    [SerializeField] private bool questThree = false;
    [SerializeField] private bool questFour = false;

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

        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questOne = finished) && (questTwo = unfinished))
        {           
            Quest2();            
        }
        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questTwo = finished) && (questThree = unfinished))
        {
            Quest3();
        }
        if (nearQuest && Input.GetKeyDown(KeyCode.E) && (questThree = finished) && (questFour = unfinished))
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
        questOne = finished;
        quest2UI.SetActive(true);

        /* if (gameObject.CompareTag("Quest1"))
         {
             questOne = finished;
             quest2UI.SetActive(true);
         }*/
        /*if (questOne = finished)
        {
            quest2UI.SetActive(true);
        }*/
    }
    private void Quest2()
    {
        quest2UI.SetActive(false);
        questTwo = finished;
        quest3UI.SetActive(true);

        /* if (gameObject.CompareTag("Quest2"))
         {
             questTwo = finished;
             quest3UI.SetActive(true);
         }*/
        /*if (questTwo = finished)
        {
            quest3UI.SetActive(true);
        }*/
    }
        
    private void Quest3()
    {
        quest3UI.SetActive(false);
        questThree = finished;
        quest4UI.SetActive(true);

        /* if (gameObject.CompareTag("Quest3"))
         {
             questThree = finished;
             quest4UI.SetActive(true);
         }*/
        /* if (questThree = finished)
         {
             quest4UI.SetActive(true);
         }*/
    }
    private void Quest4()
    {
        quest4UI.SetActive(false);
        questFour = finished;

        /*if (gameObject.CompareTag("Quest4"))
        {
           questFour = finished;
        }*/
    }
}

