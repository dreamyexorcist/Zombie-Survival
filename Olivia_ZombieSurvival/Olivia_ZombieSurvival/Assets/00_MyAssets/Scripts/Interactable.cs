using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour //Abstract class to make it inheritable from other classes.
{ 
    public virtual void Awake() //any object that inherits from Interactable will be given layer 7.
    {
        gameObject.layer = 7;
    }
    //Every class that is inherited from Interactable are defined here. ('Placeholder class).
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();


}
