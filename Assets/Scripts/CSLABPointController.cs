using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CSLABPointController : MonoBehaviour {
    protected CSLABControllerInput controllerRef;
    public GameObject prefabBullet;
    public float bulletSpeed = 2f;
    private void Awake()
    {
        controllerRef = GetComponent<CSLABControllerInput>();
    }

     void Update()
    {
        if (controllerRef.controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            Debug.Log("Trigger");
            ShootBullet();

        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(prefabBullet);
        bullet.transform.position = controllerRef.gameObject.transform.position;
        bullet.transform.rotation = controllerRef.gameObject.transform.rotation;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletSpeed);


    }
    


}
