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

    public bool isStrafeing = false;
    public bool wasStrafeing = false; //we need to know if we were strafeing, to turn our forward vector back
                                      //to velocity vector when we cross close to zero.

    public bool wasStrafeRight = false; // to tighten control, if we switch strafe directions, we need 
    public bool wasStrafeLeft = false;  // a bit of extra force to overcome momentum

    public bool isRotatingTurret = false;

    public float velocityThreshold = 0.5f; //at what speed are we essentially not moving

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
                thrust += transform.right * -playerProps.thrustForce * playerProps.lateralFactor;
                playerProps.energy -= playerProps.consumption * Time.deltaTime;

            }
            if (controller.right && playerProps.energy > 0.0f)
            {
                thrust += transform.right * playerProps.thrustForce * playerProps.lateralFactor;
                playerProps.energy -= playerProps.consumption * Time.deltaTime;
            }
        }


    }

    public virtual void handleOnSurface()
    {
        //do we have any gas in the tank?
        if (playerProps.energy > 0.0f )
        {
            //check if we are reaching zero velocity, and no keys pressed
            if (!controller.controlerOn)
            {
                //check reverse engaged and no input
                if (engagedReverse && velocity.magnitude < velocityThreshold * 0.5f)
                {
                    //give it a little anti-forward push to correct facing
                    velocity = -transform.forward * velocityThreshold;
                    engagedReverse = false;
                    isInReverse = false;
                }


                //check wasStrafeing and no input
                if (wasStrafeing && velocity.magnitude < velocityThreshold * 0.5f)
                {
                    isStrafeing = false;
                    wasStrafeing = false;
                    //give it a little forward push to correct facing
                    velocity = transform.forward * velocityThreshold;

                }
            }




            if (controller.forward)
            {
                isRotatingTurret = false;


                //if we are in reverse but swithc to forward, approach the forward threshold
                //by applying a negative forward force
                if (engagedReverse && velocity.magnitude >= velocityThreshold )
                {
                    thrust -= transform.forward * playerProps.thrustForce ;

                }
                else if (engagedReverse && velocity.magnitude < velocityThreshold)
                {
                    //give it a push in opposite direction
                    velocity = -1 * transform.forward * velocityThreshold;
                    engagedReverse = false;
                    
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
                isRotatingTurret = false;

                isInReverse = true;

                //approach the reverse threshold by applying a negative forward force                
                if (!engagedReverse && velocity.magnitude >= velocityThreshold  )
                {
                   thrust -= transform.forward * playerProps.thrustForce ;
                }
                else if (!engagedReverse && velocity.magnitude < velocityThreshold  )
                {
                    //give it a push in the opposite direction
                    velocity = -1 * transform.forward * velocityThreshold;
                    engagedReverse = true;
                }
                else if (engagedReverse)
                {
                    //once over the threshhold, apply a forward force, but we tell the
                    //geometry to flip it's facing so it appears to be moving in reverse
                    thrust += transform.forward * playerProps.thrustForce;

                }
                else
                    Debug.Log("HUH??");

                playerProps.energy -= playerProps.consumption * Time.deltaTime;



            }



            //left and right turning is a function of current velocity, thrustForce, lateralFactor, and if strafeing, that too
            float turnForce = playerProps.thrustForce * playerProps.lateralFactor;

                        
            //check if strafe. if so, reset thrust. we still have momentum in our velocity
            if (controller.strafe)
            {
                thrust.Set(0, 0, 0);
                turnForce *= playerProps.strafeFactor; //mod by strafe factor
                isStrafeing = true;                
            }
          
            turnForce *= (velocity.magnitude + 1);

           

            if (controller.left)
            {
                //handle stationary turn/targeting
                if(!controller.forward && !controller.backward && !isStrafeing && !wasStrafeing)
                {
                    //sit and spin (better to handle inside player motion, easier to handle right here)
                    thrust *= 0;
                    playerGeometry.parent.Rotate(0, -Time.deltaTime * playerProps.rotateTurretFactor, 0);
                    isRotatingTurret = true;

                }
                else
                    thrust -= transform.right * turnForce;


                playerProps.energy -= playerProps.consumption * Time.deltaTime;


                if (wasStrafeRight)
                {
                    thrust *= playerProps.thrustForce * 100;       //and more of a push if we are changing directions
                    wasStrafeRight = false;
                   
                }

                if (isStrafeing && !wasStrafeing)
                {
                    thrust *= playerProps.thrustForce * 20;        //give it a strong push to overcome inertia
                    wasStrafeing = true;                    
                }
                if (isStrafeing)
                    wasStrafeLeft = true;


            }

            if (controller.right)
            {
                //handle stationary turn/targeting
                if (!controller.forward && !controller.backward && !isStrafeing && !wasStrafeing)
                {
                    //sit and spin (better to handle inside player motion, easier to handle right here)
                    thrust *= 0;
                    playerGeometry.parent.Rotate(0, Time.deltaTime * playerProps.rotateTurretFactor, 0);
                    isRotatingTurret = true;


                }
                else
                    thrust += transform.right * turnForce;

                playerProps.energy -= playerProps.consumption * Time.deltaTime;

                if (wasStrafeLeft)
                {
                    thrust *= playerProps.thrustForce * 100;       //and more of a push if we are changing directions
                    wasStrafeLeft = false;
                   
                }

                if (isStrafeing && !wasStrafeing)
                {
                    thrust *= playerProps.thrustForce * 20;        //give it a strong push to overcome inertia                    
                    wasStrafeing = true;                    
                }
                if (isStrafeing)
                    wasStrafeRight = true;

            }

        }

        //if no key is pressed, add extra stopping force by diminishing velocity
        if (!controller.controlerOn)
            velocity *= playerProps.stoppingForce;

        //finally, if we are at a fraction of velocity threshold, we might as well stop
        if (velocity.magnitude < velocityThreshold * 0.25f && !controller.controlerOn)
        {
            velocity *= 0;
            //clear all movement flags to be sure
            isStrafeing = false;
            isInReverse = false;
            wasStrafeing = false;
            engagedReverse = false;
            wasStrafeLeft = false;
            wasStrafeRight = false;
            isRotatingTurret = false;

        }

    }

}
