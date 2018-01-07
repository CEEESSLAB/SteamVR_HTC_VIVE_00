using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The Vive headset and controllers, are GameObjects, and can be accessed and updated via scripts. 
There are many components attached to the various GameObjects in the SteamVR [CameraRig] prefab 
(which contains the GameObjects that will be tracking your headset and controllers). 

Once you have identified the trackedobj (controller or hmd), you can get information about the 
specific button states using methods of the Device Class. 

The script MUST BE ATTACHED to the Specific Device (Left or Right Controller).

Device.GetPressDown(), Device.GetPressUp(), and device.GetPress() will return a bool.
Device.GetTouchDown(), Device.GetTouchUp(), and Device.GetTouch() will return a bool. 
This is similar to GetPress in some cases, for example the trigger, GetTouch will return true before the trigger button is all the way down.
Device.GetAxis() returns the location of the user’s thumb on the touchpad (zero by default).
Device.Velocity and Device.AngularVelocity are properties that the device has denoting the controller’s current velocity and angular velocity.
Device.TriggerhapticPulse() can be used to send haptic feedback to the controllers.

*/
public class AccessVIVEControllers : MonoBehaviour {

    /* A Reference to the Controller - Need index of Tracked Object (HMD or Controllers)*/
    protected SteamVR_TrackedObject trackedobj;

    /* C# PROPERTY - ACCESSOR [GET OR SET] */
    public SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input( (int)trackedobj.index ); } 
    }

    /* GET A REFERENCE TO THE STEAMVR TRACKED OBJECT i.e. HMD, CONTROLLER */ 
    private void Awake()
    {
        trackedobj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
