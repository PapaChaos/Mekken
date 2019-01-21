using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{


    public float power = 0.0f;
    public float cooldown = -1.0f;
    public float timeout = 1.0f;  

    public playerControl controller;
    public Transform playerGeom;
    public Missile missile;

    public Vector3 angle = new Vector3(0, 0.5f, 0);
    
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

        if(controller.firePrimary && cooldown < 0)
        {
            cooldown = Time.time;
            fire();
        
        }

        

    }

    void fire()
    {
        missile.transform.position = playerGeom.position + playerGeom.forward + Vector3.up;
        missile.velocity = (playerGeom.forward + angle) * power * 20.0f;
        missile.inAir = true;
    }
    


}
