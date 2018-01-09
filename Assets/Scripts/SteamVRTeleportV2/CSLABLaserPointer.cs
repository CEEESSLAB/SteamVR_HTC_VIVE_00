using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class CSLABLaserPointer : MonoBehaviour {
    protected CSLABControllerInput controllerRef;
    //SteamVR CameraRig
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

    private void Awake()
    {
        controllerRef = GetComponent<CSLABControllerInput>();
    }
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        // position the laser  to be between the controller and the point where the raycast hits (at mid point)
        laserTransform.position = Vector3.Lerp(controllerRef.gameObject.transform.position, hitPoint, .5f);
        //point laser at the hit point
        laserTransform.LookAt(hitPoint);
        // hit.distance  == distance between the 2 points - scale the laser to fit between the 2
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    void Start()
    {
        laser = Instantiate(laserprefab);
        laserTransform = laser.transform;

        reticle = Instantiate(teleportrecticleprefab);
        teleportreticleTransform = reticle.transform;
    }

    void Update()
    {
        if (controllerRef.controller.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            RaycastHit hit;
            //shoot a ray from the controller, if it hits something make it store the point where it hit and show the laser
           
            // for teleporting - makes sure that you can only hit game objects that you can teleport to.
            if (Physics.Raycast(controllerRef.gameObject.transform.position, transform.forward, out hit, 100, teleportMask))
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
        // not pressing 
        else
        {
            laser.SetActive(false);
            // new 
            reticle.SetActive(false);
        }
        //teleport condition
        if (controllerRef.controller.GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad) && shouldTeleport == true)
        {
            Teleport();
        }
    }

    // teleport
    private void Teleport()
    {
        //set to false when we are teleporting
        shouldTeleport = false;
        //hide
        reticle.SetActive(false);
        //difference between camera rig center and player's head
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        //don't care about vertical
        difference.y = 0;
        // move camera to position of hitpoint + calculated difference
        cameraRigTransform.position = hitPoint + difference;
        // cameraRigTransform.position = hitPoint;
    }
}
