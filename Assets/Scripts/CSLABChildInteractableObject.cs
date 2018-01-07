using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
/* child */
public class CSLABChildInteractableObject : CSLABBaseInteractableObject
{
    // NEW:: define and assign member variables to denote which buttons this interactableObject child class will accept:
    EVRButtonId pickUpButton = EVRButtonId.k_EButton_SteamVR_Trigger;
   

    // pickup method - > we receive the controller Script as an argument
    public override void ButtonDown(CSLABControllerInput controllerRef)
    {
        Debug.Log("TRIGGER PRESSED");
        // it should not be affected by in game physics
        rb.isKinematic = true;
        //parent the object to the transform of the gameObject that has the controller script attatched to it (our HTC controller)
        transform.SetParent(controllerRef.gameObject.transform);
    }

    // release method - > we receive the controller Script as an argument
    public override void ButtonUp(CSLABControllerInput controllerRef)
    {
        // ensure that we are still bound to the same controller
        if (transform.parent == controllerRef.gameObject.transform)
        {
            //reset
            rb.isKinematic = originalKinematicState;
            // ensure that original parent was not the controller
            if (parent != controllerRef.gameObject.transform)
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

    // pickup method - > we receive the controller Script as an argument
    public override void ButtonDown(EVRButtonId button,CSLABControllerInput controllerRef)
    {
        if(pickUpButton ==button)
        {
            PickUp(controllerRef);
        }
    }

    // release method - > we receive the controller Script as an argument
    public override void ButtonUp(EVRButtonId button,CSLABControllerInput controllerRef)
    {
        if (pickUpButton == button)
        {
            Release(controllerRef);
        }

    }
    private void PickUp(CSLABControllerInput controllerRef)
    {
        Debug.Log("TRIGGER PRESSED VIA PASSING THE BUTTON");
        // it should not be affected by in game physics
        rb.isKinematic = true;
        //parent the object to the transform of the gameObject that has the controller script attatched to it (our HTC controller)
        transform.SetParent(controllerRef.gameObject.transform);

    }

    private void Release(CSLABControllerInput controllerRef)
    {
        Debug.Log("TRIGGER RELEASED VIA PASSING THE BUTTON");
        // ensure that we are still bound to the same controller
        if (transform.parent == controllerRef.gameObject.transform)
        {
            //reset
            rb.isKinematic = originalKinematicState;
            // ensure that original parent was not the controller
            if (parent != controllerRef.gameObject.transform)
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
}
