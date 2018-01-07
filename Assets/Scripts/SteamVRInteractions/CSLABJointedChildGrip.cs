using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class CSLABJointedChildGrip : CSLABBaseInteractableObject
{

    EVRButtonId pickUpButton = EVRButtonId.k_EButton_Grip;
    //  we receive the controller Script as an argument
    public override void ButtonDown(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        if (pickUpButton == button)
        {
            PickUp(controllerRef);
        }
    }

    //  we receive the controller Script as an argument
    public override void ButtonUp(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        if (pickUpButton == button)
        {
            Release(controllerRef);
        }

    }

    private void PickUp(CSLABControllerInput controllerRef)
    {
       
        var joint = AddFixedJoint(controllerRef);
        joint.connectedBody = GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint(CSLABControllerInput controllerRef)
    {
        FixedJoint fx = controllerRef.gameObject.AddComponent<FixedJoint>();
       // fx.breakForce = 20000;
     //   fx.breakTorque = 20000;
        return fx;
    }

    private void Release(CSLABControllerInput controllerRef)
    {
        if (controllerRef.gameObject.GetComponent<FixedJoint>())
        {
            controllerRef.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(controllerRef.gameObject.GetComponent<FixedJoint>());
            //reset speed and rotation -> to that of Controller
            GetComponent<Rigidbody>().velocity = controllerRef.controller.velocity;
            GetComponent<Rigidbody>().angularVelocity = controllerRef.controller.angularVelocity;

        }
    }
}
