using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{


    public Transform target;
    public rocketConeTarget visibilityCone;
    public Transform playerGeom;

    private Quaternion initRotation = new Quaternion();

    public float dot = 90;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void Update()
    {

        Vector3 rocketFwd = transform.forward;
        Vector3 playerFwd = playerGeom.forward;

       
        if (visibilityCone.lockedOn)
        {

            dot = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(rocketFwd, playerFwd));

            if ( dot > 160)
                visibilityCone.lockedOn = false;

        }
        


    }


    // Update is called once per frame
    void LateUpdate()
    {

        if (visibilityCone.lockedOn)
        {
            initRotation = transform.rotation;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Lerp(initRotation, transform.rotation, Time.deltaTime * 20.0f);
        }
        else
        {
            
            initRotation = Quaternion.identity;
            transform.localRotation = Quaternion.Lerp( transform.localRotation, initRotation, Time.deltaTime * 20.0f);
        }

        

    }
}
