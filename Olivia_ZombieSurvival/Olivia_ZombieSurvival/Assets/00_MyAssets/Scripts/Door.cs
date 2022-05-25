using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    private bool canBeInteractedWith = true; //to lock interaction when animation is running
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (canBeInteractedWith)
        {
            isOpen = !isOpen; //toggle isOpen state either way

            Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward); //translates doors local pos into a wolrd pos.
            Vector3 playerTransformDirection = FirstPersonController.instance.transform.position - transform.position; //get position of FPC at that frame and subsctract doors dot position.
            float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection); //get dot product to determine which way door opens/closes.

            anim.SetFloat("dot", dot);
            anim.SetBool("isOpen", isOpen); //animater swings the door

            StartCoroutine(AutoClose());
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    private IEnumerator AutoClose()
    {
        while (isOpen)
        {
            yield return new WaitForSeconds(5);

            if (Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > 5)
            {
                isOpen = false;
                anim.SetFloat("dot", 0);
                anim.SetBool("isOpen", isOpen);
            }

        }
    }

    private void Animator_LockInteraction()
    {
        canBeInteractedWith = false;
    }

    private void Animator_UnlockInteraction()
    {
        canBeInteractedWith = true;
    }

}
