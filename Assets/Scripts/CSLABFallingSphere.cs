using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSLABFallingSphere : CSLABBaseInteractableObject
{


    private bool initiatedFalling = false;
    public Transform FallSphere;
    public Renderer objRenderer;
    protected Color originalColor;
    protected Material objectMaterial;
    public Color downColor;
    // Use this for initialization
    void Awake()
    {
        objectMaterial = objRenderer.material;
        originalColor = objectMaterial.color;
    }

    public override void RayStay(RaycastHit hit, CSLABControllerInput controllerRef = null)
    {
        objectMaterial.color = downColor;


        if (controllerRef == null)
        {
            if (!initiatedFalling)
            {
                initiatedFalling = true;
                //public void InvokeRepeating(string methodName, float time, float repeatRate); 
                InvokeRepeating("RepeatingFunction",.2f,.5f);


            }
        }
    }

    void RepeatingFunction()
    {
        float x = Random.Range(transform.position.x-1, transform.position.x + 1);
        float z = Random.Range(transform.position.z - 1, transform.position.z + 1);
        float y = 10f;
        Vector3 newPosition = new Vector3(x, y, z);

        Instantiate(FallSphere,newPosition, Quaternion.identity);
       
    }

    public override void RayExit(CSLABControllerInput controllerRef = null)
    {
        initiatedFalling = false;
        objectMaterial.color = originalColor;
        CancelInvoke();
    }
}
