using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour //Abstract class to make it inheritable from other classes.

    //Every class that is inherited from Interactable are defined here. ('Placeholder class).
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();





}
