using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* We’re going to start with the script that you put on any object 
 * you want to have some sort of interaction with the controllers:
 * it’s more flexible to put it on the object and only receive input from the controller. */


public class InteractableObject : MonoBehaviour {
    // class variables for ANY interactable object
    // the rigidbody of this object
    protected Rigidbody rb;
    // the object's original kinematic state
    protected bool originalKinematicState;
   // the object's parent's Transfrom
    protected Transform parent;


    
    void Awake () {
        // assign the class variables
        rb = GetComponent<Rigidbody>();
        originalKinematicState = rb.isKinematic;
        parent = transform.parent;
}

    // pickup method - > we receive the controller Script as an argument
    public void PickUp(ControllerInput controllerRef )
    {
        // it should not be affected by in game physics
        rb.isKinematic = true;
        //parent the object to the transform of the gameObject that has the controller script attatched to it (our HTC controller)
        transform.SetParent(controllerRef.gameObject.transform);
    }

    // release method - > we receive the controller Script as an argument
    public void Release(ControllerInput controllerRef)
    {
        // ensure that we are still bound to the same controller
        if(transform.parent == controllerRef.gameObject.transform)
        {
            //reset
            rb.isKinematic = originalKinematicState;
            // ensure that original parent was not the controller
            if(parent!= controllerRef.gameObject.transform)
            {
                //reset to original parent
                transform.SetParent(parent);
            }
            else
            {
                transform.SetParent(null);
            }
            // for throwing -> use the velocity and angular velocity from the controller
            rb.velocity = controllerRef.controller.velocity;
            rb.angularVelocity = controllerRef.controller.angularVelocity;
        }
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
