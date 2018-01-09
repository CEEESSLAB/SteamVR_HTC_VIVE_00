using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSLABRaycastableInteractableObject : CSLABBaseInteractableObject
{
    public Color headRayColor;
    public Color handRayColor;
    public Renderer objRenderer;
    protected Color originalColor;
    protected Material objectMaterial;


    // Use this for initialization
    void Awake()
    {
        objectMaterial = objRenderer.material;
        originalColor = objectMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void RayEnter(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        Color c;
        if (controllerRef == null)
        {
            c = headRayColor;

        }
        else
        {
            c = handRayColor;
        }
        objectMaterial.color = c;
    }
    public override void RayStay(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        if (controllerRef == null)
        {

        }
        else
        {

        }
    }
    public override void RayExit(CSLABControllerInput controllerRef = null)
    {
        objectMaterial.color = originalColor;
    }
}

