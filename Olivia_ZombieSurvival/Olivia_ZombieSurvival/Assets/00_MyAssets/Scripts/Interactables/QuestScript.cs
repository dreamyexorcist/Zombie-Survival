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
    private bool finished = true;

    [SerializeField] private bool questOne = false;
    [SerializeField] private bool questTwo = false;
    [SerializeField] private bool questThree = false;
    [SerializeField] private bool questFour = false;    

    void Update()
    {
        if (nearQuest && Input.GetKeyDown(KeyCode.E))
        {
            Quest1();
        }

        if ((questOne = finished) && nearQuest && Input.GetKeyDown(KeyCode.E))
        {
            Quest2();
        }

        if ((questTwo = finished) && nearQuest && Input.GetKeyDown(KeyCode.E))
        {
            Quest3();
        }
        if ((questThree = finished) && nearQuest && Input.GetKeyDown(KeyCode.E))
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
        if (gameObject.CompareTag("Quest1"))
        {
            questOne = finished;
            quest1UI.SetActive(false);
            quest2UI.SetActive(true);
        }
    }
    private void Quest2()
    {
        if (gameObject.CompareTag("Quest2"))
        {
            questTwo = finished;
            quest2UI.SetActive(false);
            quest3UI.SetActive(true);
        }
        Destroy(this.gameObject);
    }

    private void Quest3()
    {
        if (gameObject.CompareTag("Quest3"))
        {
            questThree = finished;
            quest3UI.SetActive(false);
            quest4UI.SetActive(true);
        }
    }
    private void Quest4()
    {
        if (gameObject.CompareTag("Quest4"))
        {
            questFour = finished;
            quest4UI.SetActive(false);           
        }
        Destroy(this.gameObject);
    }
}
