using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPhysics : MonoBehaviour
{

    //physics properties and values read from controller input
    //and used by playerMovement - basically steers like a car

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector

    public playerControl controller;
    public playerProperties playerProps;
    public Transform playerGeometry;


    public bool isInReverse = false;     // velocity determines our forward facing, when in reverse 
    public bool engagedReverse = false;  // we need to inform movement to maintain the opposite facing


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //reset thrust each frame
        thrust.Set(0, 0, 0);

        if (playerProps.onSurface)
            handleOnSurface();
        else
            handleInAir();

    }

    void handleInAir()
    {
        //read controller values and set physics thrust
        if (playerProps.energy > 0.0f )
        {
            //forward when in air means vertical thruster            
            if (controller.forward)
            {
                thrust.y = playerProps.thrustForce;
                playerProps.energy -= playerProps.consumption * Time.deltaTime;

            }
            if (controller.left && playerProps.energy > 0.0f)
            {
                thrust += transform.right * -playerProps.lateralThrustForce;
                playerProps.energy -= playerProps.consumption * Time.deltaTime;

            }
            if (controller.right && playerProps.energy > 0.0f)
            {
                thrust += transform.right * playerProps.lateralThrustForce;
                playerProps.energy -= playerProps.consumption * Time.deltaTime;
            }
        }


    }

    void handleOnSurface()
    {
        //reset reverse when we cross close to zero velocity
        //this is assuming no keys are pressed or we are out of gas
        if (isInReverse && velocity.magnitude < 0.01f)
        {
            isInReverse = false;
            engagedReverse = false;

        }

        if (playerProps.energy > 0.0f && playerProps.onSurface)
        {
            if (controller.forward)
            {
                //if we were in reverse, approach the forward threshold by applying a negative forward force
                if (isInReverse && velocity.magnitude >= 0.2f && engagedReverse)
                {
                    thrust -= transform.forward * playerProps.lateralThrustForce;

                }
                else if (engagedReverse)
                {
                    //give it a push in opposite direction
                    velocity *= -10;
                    engagedReverse = false;
                    isInReverse = false;

                }
                else
                {
                    //forward on the ground means thrust on the forward vector
                    thrust += transform.forward * playerProps.thrustForce;
                    playerProps.energy -= playerProps.consumption * Time.deltaTime;

                }
            }
            if (controller.backward)
            {

                isInReverse = true;

                //approach the reverse threshold by applying a negative forward force
                if (isInReverse && velocity.magnitude >= 0.2f && !engagedReverse)
                {
                    isInReverse = false;
                    thrust -= transform.forward * playerProps.lateralThrustForce;

                }
                else if (isInReverse && velocity.magnitude < 0.2f && !engagedReverse)
                {
                    //flip forward facing and give it a push
                    velocity *= -10;
                    engagedReverse = true;
                }
                else if (engagedReverse)
                {
                    //once over the threshhold, apply a forward force, but we tell the
                    //geometry to flip it's facing so it appears to be moving in reverse
                    thrust += transform.forward * playerProps.lateralThrustForce;

                }
                else
                    Debug.Log("HUH??");

                playerProps.energy -= playerProps.consumption * Time.deltaTime;



            }
            if (controller.left)
            {
                thrust -= transform.right * playerProps.lateralThrustForce * (velocity.magnitude + 1);
                playerProps.energy -= playerProps.consumption * Time.deltaTime;


            }
            if (controller.right)
            {
                thrust += transform.right * playerProps.lateralThrustForce * (velocity.magnitude + 1);
                playerProps.energy -= playerProps.consumption * Time.deltaTime;
            }

        }
    }

}
