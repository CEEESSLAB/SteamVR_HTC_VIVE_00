using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
/* this class will be the base class for any more specific child InteractableObject */
public class CSLABBaseInteractableObject : MonoBehaviour
{

    // class variables for ANY interactable object
    // the rigidbody of this object
    protected Rigidbody rb;
    // the object's original kinematic state
    protected bool originalKinematicState;
    // the object's parent's Transfrom
    protected Transform parent;



    void Awake()
    {
        // assign the class variables
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            originalKinematicState = rb.isKinematic;
            parent = transform.parent;
        }
    }

    public virtual void ButtonDown(CSLABControllerInput controllerRef)
    {
        // EMPTY WILL BE overriden
    }

    public virtual void ButtonUp(CSLABControllerInput controllerRef)
    {
        // EMPTY WILL BE overriden
    }

    public virtual void ButtonDown(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        // EMPTY WILL BE overriden
    }

    public virtual void ButtonUp(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        // EMPTY WILL BE overriden
    }

    public virtual void RayEnter(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        // EMPTY WILL BE overriden
    }
    public virtual void RayStay(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        // EMPTY WILL BE overriden
    }
    public virtual void RayExit(CSLABControllerInput controllerRef = null)
    {
        // EMPTY WILL BE overriden
    }
}