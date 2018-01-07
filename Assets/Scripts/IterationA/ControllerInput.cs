using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the script to be attatched to a controller */
public class ControllerInput : MonoBehaviour {

    protected SteamVR_TrackedObject trackedObj;
    protected InteractableObject heldObject;

    // get the Device
    public SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }


	// Use this for initialization
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        heldObject = null;
    }

    //when contoller collides with an obj
    private void OnTriggerStay(Collider other)
    {
        // get the interactableObject instance attatched to the collided object
        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if(interactable != null)
        {  
            // if we have pressed down -> pickup
            if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                //call the method ... 
                interactable.PickUp(this);
                heldObject = interactable;
            }
           
        }
    }

    // Update is called once per frame
    void Update () {
        if(heldObject!=null)
        {
            if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                heldObject.Release(this);
                heldObject = null;
            }
        }
		
	}
}
