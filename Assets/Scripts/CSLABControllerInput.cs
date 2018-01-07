using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CSLABControllerInput : MonoBehaviour {

    //NEW:: a list of all possible buttons that this controller class can take as input:
    protected List<EVRButtonId> buttonsTracked;
   // reference to the TrackedObject class attached to the controller
    protected SteamVR_TrackedObject trackedObj;
    // reference to the Interactable Object we are holding
    protected CSLABBaseInteractableObject heldObject;
    protected CSLABBaseInteractableObject interactable;

    // get the Device -> this accessor method is invoked every time we use the controller variable-
    // so it will always contain the most uptodate info
    public SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }


    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        heldObject = null;
        //NEW:: populate the list of possible Buttons (we could add more ...)
        buttonsTracked = new List<EVRButtonId>()
        {
            EVRButtonId.k_EButton_SteamVR_Trigger,
            EVRButtonId.k_EButton_Grip,
            EVRButtonId.k_EButton_SteamVR_Touchpad,
           EVRButtonId.k_EButton_ApplicationMenu
        };

       
    }

    //when contoller collides and remains within an obj
    private void OnTriggerStay(Collider other)
    {
        // get the interactableObject instance attatched to the collided object
        //interactable = other.GetComponent<CSLABBaseInteractableObject>();
        if (interactable != null)
        {
            /*NEW::  iterate through the list of buttons that this controller is able to receive as input*/
            for(int i = 0; i < buttonsTracked.Count; i++)
            {
                EVRButtonId button = buttonsTracked[i];
                /* if this is the button being triggered by the controller 
                 * then invoke the ButtonDown method in the Interactable Object class, 
                 * and pass button  on to the interactable object (to check if this interactable object responds to this button*/

                if ( controller.GetPressDown(button) )
                {
                    heldObject = interactable;
                    interactable.ButtonDown(button,  this);

                } else if (button == EVRButtonId.k_EButton_SteamVR_Touchpad && controller.GetTouch(button))
                {
                    heldObject = interactable;
                    interactable.ButtonDown(button, this);
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<CSLABBaseInteractableObject>();
    }

    void OnTriggerExit(Collider other)
    {
        interactable = null;
        heldObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null)
        {
            /*NEW::  iterate through the list of buttons that this controller is able to receive as input*/
            for (int i = 0; i < buttonsTracked.Count; i++)
            {
              EVRButtonId button = buttonsTracked[i];
              
               if (controller.GetPressUp(button))
               {
                    heldObject.ButtonUp(button,this);
                    heldObject = null;
                }
            }
        }

    }
}
