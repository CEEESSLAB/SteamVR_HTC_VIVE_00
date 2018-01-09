using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSLABRaycastableCanvasObject : CSLABBaseInteractableObject{

    public RectTransform headIndicatorRect; //the transform of the object affected by the head
    public RectTransform[] handIndicatorRects; // the transforms of the objects affected by the hands (will have 2 - one per hand)
    public RectTransform canvasRect;
	
    //differentiate between left and right controller by using the SteamVR_Controller class and getting the index of the left controller
    protected int leftControllerIndex
    {
        get
        {
            return SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        }
    }

    public override void RayStay(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        //convert position in world to position on the canvas
        Vector2 positionRayCastRelativeToCanvas = canvasRect.InverseTransformPoint(hit.point);
        if (controllerRef == null)
        {
            //head
            headIndicatorRect.anchoredPosition = positionRayCastRelativeToCanvas;
        }
        else
        {
            int controllerIndex = -1 ;
            //hand
            if (controllerRef.controller.index ==leftControllerIndex)
            {
                controllerIndex = 0;
            }
            else
            {
                controllerIndex = 1;
            }

            // need to ensure there are enough rects (index is 0,1 and length of array is 2)
            if(controllerIndex<handIndicatorRects.Length)
            {
                handIndicatorRects[controllerIndex].anchoredPosition = positionRayCastRelativeToCanvas;

            }

        }
    }
}
