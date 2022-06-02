using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    [SerializeField] bool showObjective = false;
    [SerializeField] Texture image;
    [SerializeField] UI objective;

    private int collision;

    private void Start()
    {
        showObjective = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && showObjective == false && collision == 0)
            showObjective = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            showObjective = false;
        collision = 1;
    }

    private void OnGUI()
    {
        //if (showObjective == true)
           
    }

}

