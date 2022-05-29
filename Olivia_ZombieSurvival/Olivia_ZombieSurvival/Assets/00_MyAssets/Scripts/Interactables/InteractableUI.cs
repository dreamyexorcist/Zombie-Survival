using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableUI : Interactable
{
    private bool hasInstruction = true;
    private bool canBeInteractedWith = true;

    private void Start()
    {
        GetComponent<Interactable>();
        GetComponent<Canvas>();
        GetComponent<TextMeshProUGUI>();
    }

    public override void OnFocus()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteract()
    {
        if (canBeInteractedWith)
        {
            //when looking at interactible set instructions canvas active, show UI text for x sedconds
            
            StartCoroutine(ShowInstruction());
        }
    }

    public override void OnLoseFocus()
    {
        throw new System.NotImplementedException();
    }


    private IEnumerator ShowInstruction()
    {
        while (hasInstruction)
        {
            yield return new WaitForSeconds(5);

            if (hasInstruction == true)
            {
                canBeInteractedWith = true;
                //
                
            }
               
        }
    }
}
