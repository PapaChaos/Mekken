using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{


    public float power = 1.0f;    
    public float timeout = 1.0f;  

    public playerControl controller;
    public Transform playerGeom;    
    public Transform missilePool;

    private Missile missile;
    private float cooldown = -1.0f;

    //TODO: decide how to set this based on mecha constructor, maybe just LEFT is secondary
    //      we could make a subclass, but just for one boolean??
    public bool isPrimary = false;
        
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - cooldown > timeout)
        {
            cooldown = -1.0f;
            
        }

        if(controller.firePrimary && cooldown < 0 && isPrimary)
        {
            cooldown = Time.time;

            setMissile();

            fire();
        
        }

        if (controller.fireSecondary && cooldown < 0 && !isPrimary)
        {
            cooldown = Time.time;

            setMissile();

            fire();

        }



    }

    void setMissile()
    {
        foreach(Transform child in missilePool)
        {
            Missile tmisl = child.GetComponent<Missile>();
            //get the first unused missile in the pool
            if (tmisl.isInUse == false)
            {
                //got one so scoot
                missile = tmisl;
                tmisl.isInUse = true;
                return;
            }
        }


    }

    void fire()
    {

        Debug.Log("WHY FIRE?");

        //move the missle into initial position
        missile.transform.position = playerGeom.position + playerGeom.forward + Vector3.up;
        missile.transform.forward = playerGeom.forward;

        //own the missile, the controller PhysicsPlayer who fired it
        missile.owner = controller.transform;

        //tell the missle to fire
        missile.fireMissile(playerGeom.forward, power);

    }
    


}
