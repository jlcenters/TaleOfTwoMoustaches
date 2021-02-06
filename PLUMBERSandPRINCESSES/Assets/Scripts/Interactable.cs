using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string interactDescr;
    public UnityEvent onInteract;

    public void Interact()
    {
        //if the object is interacted with, prompt action
        if(onInteract != null)
        {
            onInteract.Invoke();
        }
    }

}
