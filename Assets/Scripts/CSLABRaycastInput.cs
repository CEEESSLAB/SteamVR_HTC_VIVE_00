using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSLABRaycastInput : MonoBehaviour {

    protected GameObject hitObj;
    protected RaycastHit rayHit;
    // reference to the Interactable Object we are gazing at
    protected CSLABBaseInteractableObject gazedUponObject;
    
    // if is a controller then this ref will be valid  - if is the HMD then not
    protected CSLABControllerInput controllerRef;

    private void Awake()
    {
        controllerRef = GetComponent<CSLABControllerInput>();
    }

    //trigger on the first frame that we are pointing at the object
    protected void RayEnter(RaycastHit hit)
    {

        gazedUponObject = hitObj.GetComponent<CSLABBaseInteractableObject>();
        if (gazedUponObject != null)
        {
            gazedUponObject.RayEnter(hit, controllerRef ?? null);
        }
    }
    //trigger every frame while pointing
    protected void RayStay(RaycastHit hit)
    {
       
        if (gazedUponObject != null)
        {
            gazedUponObject.RayStay(hit, controllerRef ?? null);
        }
    }
    //trigger on the first frame that we stop pointing at the object
    protected void RayExit()
    {
        if (gazedUponObject != null)
        {
            gazedUponObject.RayExit(controllerRef ?? null);
            gazedUponObject = null;
            hitObj = null;
        }
    }

     void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit))
        {
            //if the obj is the same as the one we hit last frame
            if(hitObj!=null && hitObj ==rayHit.transform.gameObject)
            {
                RayStay(rayHit);
            }
            else
            {
                //is on the same 
                RayExit();
                hitObj = rayHit.transform.gameObject;
                RayEnter(rayHit);
            }
        }
        // we are not hitting anything
        else
        {
            //is on the same 

            RayExit();
            
        }

    }

}
