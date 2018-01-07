using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class CSLABColorChildInteractableObject : CSLABBaseInteractableObject
{
    EVRButtonId changeColorButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
   // public EVRButtonId changeColorButton;
    // pickup method - > we receive the controller Script as an argument
    private List<Color> colorSwatches;
    private int cIndex;

    public void Start()
    {
        colorSwatches = new List<Color>()
        {
            Color.red,
            Color.blue,
            Color.white,
            Color.magenta,
            Color.cyan,
            Color.green,
            Color.yellow

        };
        cIndex = 0;
    }

    public override void ButtonDown(EVRButtonId button,CSLABControllerInput controllerRef)
    {
        if (changeColorButton == button)
        {
           Vector2 axes= controllerRef.controller.GetAxis();
            changeColor(controllerRef,axes);
        }
    }

    // release method - > we receive the controller Script as an argument
    public override void ButtonUp(EVRButtonId button, CSLABControllerInput controllerRef)
    {
       //

    }

    private void changeColor(CSLABControllerInput controllerRef, Vector2 axes)
    {
        Debug.Log("THIS IS NOW ACTIVE VIA TOUCH:: " + axes);
      //  if (cIndex == colorSwatches.Count) { cIndex = 0; }
      //  this.gameObject.GetComponent<Renderer>().material.color = colorSwatches[cIndex];
       // cIndex++;
      
    }
}
