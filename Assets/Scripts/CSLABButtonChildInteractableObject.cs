using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CSLABButtonChildInteractableObject : CSLABBaseInteractableObject
{
    // NEW:: define and assign member variables to denote which buttons this interactableObject child class will accept:
    EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    // variables to set ... (in inspector)
    public Transform buttonModel;
    public Vector3 buttonDownPos;
    public float buttonClickSpeed;
    private Vector3 currentButtonDestination;
    private Vector3 buttonStartPos;

    public Transform testCapsule;

    // Use this for initialization
    void Start () {
        currentButtonDestination = buttonModel.localPosition;
        buttonStartPos = buttonModel.localPosition;

    }
	
	// Update is called once per frame
	void Update () {
		if(buttonModel.localPosition!=currentButtonDestination)
        {
            Vector3 moveDownPosition = Vector3.MoveTowards(buttonModel.localPosition, currentButtonDestination, buttonClickSpeed * Time.deltaTime);
            buttonModel.localPosition = moveDownPosition;
        }
	}

    // pickup method - > we receive the controller Script as an argument
    public override void ButtonDown(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        Debug.Log("INITIAL COLLISION");
        if (triggerButton == button)
        {
            //where we want it to go
            Debug.Log("ButtonDown");
            currentButtonDestination = buttonDownPos;
        }
    }

    // release method - > we receive the controller Script as an argument
    public override void ButtonUp(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        if (triggerButton == button)
        {
            //where we want it to go
            Debug.Log("ButtonUp");
            currentButtonDestination = buttonStartPos;
            DealWithPress();
        }

    }
    private void DealWithPress()
    {
        Instantiate(testCapsule);
    }
}
