using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
    private SteamVR_TrackedObject trackedOBJ;
    public Transform cameraRigTransform;
    public GameObject teleportrecticleprefab;
    private GameObject reticle;
    private Transform teleportreticleTransform;
     //player's head
    public Transform headTransform;
    //reticle offset from the floor
    public Vector3 teleportreticleOffset;
    // filter areas on which teleports are allowed
    public LayerMask teleportMask;
    // can you teleport to that location?
    private bool shouldTeleport;

    public GameObject laserprefab;
    private GameObject laser;
    private Transform laserTransform;
    //position where laser hits
    private Vector3 hitPoint;

    private SteamVR_Controller.Device Controller
    {

        get { return SteamVR_Controller.Input((int)trackedOBJ.index); }
    }

    private void Awake()
    {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();
    }
  
	
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedOBJ.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    void Start()
    {
        laser = Instantiate(laserprefab);
        laserTransform = laser.transform;

        reticle = Instantiate(teleportrecticleprefab);
        teleportreticleTransform = reticle.transform;
        
    }
    // Update is called once per frame
    void Update () {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            //shoot a ray from the controller, if it hits something make it store the point where it hit and show the laser
            //  if(Physics.Raycast(trackedOBJ.transform.position,transform.forward,out hit, 10))
            // for teleporting - makes sure that you can only hit game objects that you can teleport to.
            if (Physics.Raycast(trackedOBJ.transform.position, transform.forward, out hit, 100,teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                // new
                //show reticle

                reticle.SetActive(true);
                //move reticle to where raycast hit +offset
                teleportreticleTransform.position = hitPoint + teleportreticleOffset;
                shouldTeleport = true;
            }
            
        }
        else
        {
            laser.SetActive(false);
            // new 
            reticle.SetActive(false);
        }

        if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)&& shouldTeleport ==true)
        {
            Teleport();
        }
		
	}

    private void Teleport()
    {
        //set to false when we are teleporting
        shouldTeleport = false;
        //hide
        reticle.SetActive(false);
        //difference between camera center and player's head
        Vector3 difference = cameraRigTransform.position -headTransform.position;
        //don't care about vertical
        difference.y = 0;
        // move camera to position of hitpoint + calculated difference
        cameraRigTransform.position = hitPoint + difference;
       // cameraRigTransform.position = hitPoint;
    }
}
