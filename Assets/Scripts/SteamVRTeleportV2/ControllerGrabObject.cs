using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedOBJ;
    // stores the game object that the trigger is collided with
    private GameObject collidingObj;
    //reference to the gameobject being grabbed
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedOBJ.index); }
    }

    private void Awake()
    {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();
    }
    // make potential grab object
    private void SetCollidingObject(Collider col)
    {
        // if collidingObj already exsists or does not have a rigid body
        if(collidingObj || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObj = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
       if(!collidingObj)
        {
            return;
        }
        collidingObj = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObj;
        collidingObj = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if(GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            //reset speed and rotation -> to that of Controller
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;

        }
    }







    // Update is called once per frame
    void Update () {
        // lets grab an object based on trigger
        if(Controller.GetHairTriggerDown())
        {
            if(collidingObj)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

    }
}
