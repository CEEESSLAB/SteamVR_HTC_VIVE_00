using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CSLABChildBullsEye : CSLABBaseInteractableObject
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="bulletHit")
        {
            Debug.Log("HIT");
            Destroy(collision.gameObject);

        }
    }
    
}
