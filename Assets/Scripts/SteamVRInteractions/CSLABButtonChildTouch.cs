using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class CSLABButtonChildTouch : CSLABBaseInteractableObject
{
    // NEW:: define and assign member variables to denote which buttons this interactableObject child class will accept:
    EVRButtonId touchButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    Color originalColor;

    private void Awake()
    {
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // we receive the controller Script as an argument
    public override void ButtonDown(EVRButtonId button, CSLABControllerInput controllerRef)
    {
       
        if (touchButton == button)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;

        }
    }

    //  we receive the controller Script as an argument
    public override void ButtonUp(EVRButtonId button, CSLABControllerInput controllerRef)
    {
        if (touchButton == button)
        {
            this.gameObject.GetComponent<Renderer>().material.color = originalColor;
          
        }

    }
}
