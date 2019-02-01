using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{


    public Transform target = null;
    public rocketConeTarget visibilityCone;
    public Transform playerGeom;

    private Quaternion initRotation = new Quaternion();

    public float dot = 90;
    public float angle = 90;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void Update()
    {

        Vector3 rocketFwd = transform.forward;
        Vector3 playerFwd = playerGeom.forward;
        Vector3 playerRight = playerGeom.right;
        Vector3 playerLeft = playerRight * -1;

       
        if (visibilityCone.lockedOn)
        {

            dot = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(rocketFwd, playerFwd));

            //this is a Unity function that does the same thing
            angle = Vector3.Angle(rocketFwd, playerFwd);

            if ( dot > 160)
                visibilityCone.lockedOn = false;

            //check the angle between the rocket and the perpendicular vector to playerFwd, aka "right"
            //TODO: confirm same mathe if rocket launcher on the other arm (prolly will need "left" aka right * -1

            dot = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(rocketFwd, playerLeft)); //when rocket is on the right
            if (dot < 60)
                visibilityCone.lockedOn = false;


        }
        


    }


    // Update is called once per frame
    void LateUpdate()
    {

        if (!target)
            return; //early out

        if (visibilityCone.lockedOn)
        {
            //lock on to the target using the GLOBAL coordinate system
            initRotation = transform.rotation;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Lerp(initRotation, transform.rotation, Time.deltaTime * 20.0f);
        }
        else
        {
            //reset rotation using the LOCAL coordinate system
            initRotation = Quaternion.identity;
            transform.localRotation = Quaternion.Lerp( transform.localRotation, initRotation, Time.deltaTime * 20.0f);
        }

        

    }
}
