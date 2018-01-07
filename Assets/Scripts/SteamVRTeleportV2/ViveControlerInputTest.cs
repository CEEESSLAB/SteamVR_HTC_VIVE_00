using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControlerInputTest : MonoBehaviour {

    private SteamVR_TrackedObject trackedOBJ;

    private SteamVR_Controller.Device Controller {
        
        get {  return SteamVR_Controller.Input((int)trackedOBJ.index); } 
    }

    private void Awake()
    {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        if (Controller.GetAxis() != Vector2.zero) {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " TRIGGRR PRESSED");
        }
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " TRIGGRR RELEASED");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " GRPPPED");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " UPPED");
        }
    }
}
