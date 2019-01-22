using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    

    //these may be a function of the player, if they have control of cannon angle,
    //or it is an inherent property of the projectile/weapon system
    public Vector3 angle = new Vector3(0, 0.5f, 0);
    public float powerFactor = 20.0f;

    //these we will eventual make private
    public bool inAir = false;    
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 finalForce;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            handleMovementAir();            
        }
        
    }


    void handleMovementAir()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, -9.8f, 0);

        acceleration = finalForce;
        //add more forces here

        //append to velocity
        velocity += acceleration * Time.deltaTime;
        
        //move the object
        transform.position += velocity * Time.deltaTime;

        if (transform.position.y < 0.5f)
        {
            Debug.Log("BOOM!!!");
            inAir = false;
        }


    }

    public void fireMissile(Vector3 direction, float power)
    {

        velocity = (direction + angle) * power * powerFactor;
        inAir = true;
    }

}
