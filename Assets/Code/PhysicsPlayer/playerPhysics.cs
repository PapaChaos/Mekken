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
    public bool wasInReverse = false;  // we need to inform movement to maintain the opposite facing

    public bool isInForward = false;     // velocity determines our forward facing, when in reverse 
    public bool wasInForward = false;  // we need to inform movement to maintain the opposite facing

    public bool updateFacing = false;

    public bool isStrafeing = false;
    public bool wasStrafeing = false; //we need to know if we were strafeing, to turn our forward vector back
                                      //to velocity vector when we cross close to zero.

    public bool wasStrafeRight = false; // to tighten control, if we switch strafe directions, we need 
    public bool wasStrafeLeft = false;  // a bit of extra force to overcome momentum

    public bool isRotatingTurret = false;

    public float velocityThreshold = 0.5f; //at what speed are we essentially not moving


    public GameManager gameManager;

    void Awake()
    {
		gameManager = FindObjectOfType<GameManager>();
    }


	void FixedUpdate()
	{
		if (!gameManager.debugIgnoreCountdown)
		{
			if (!gameManager.getGameOver() && gameManager.getRoundReady())
				handleOnSurface();
		}
		else
			if (!gameManager.getGameOver())
				handleOnSurface();
		//using fixed update bellow, so anything that must be handled per frame
		//should be handled here 
	}


	// Update is called once per frame, fixed update is called when the physics updates at a set rate
	// this helps to solve things like jitter, and in this case, a frame rendering when the geometry is
	// changing facing from forward to reverse and v/v. I would prefer not to do this, but it's a fix, done and done.

	//removed as we shouldn't have any handling in air.
	/*
    void Update()
    {


		
        //reset thrust each frame
        thrust *= 0;

        if ( !gameManager.getGameOver() )
        {
            if (playerProps.onSurface)
                handleOnSurface();
            else
                handleInAir();
        }

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
	*/

	public virtual void handleOnSurface()
    {
        //do we have any gas in the tank?
        if (playerProps.energy > 0.0f )
        {

            updateFacing = true;

            if (controller.forward)
            {

                

                isRotatingTurret = false;
                isInForward = true;
                isInReverse = false;

                if (wasInReverse && velocity.magnitude >= velocityThreshold)
                {
                    //apply a brakeing force until we cross the zero threshold
                    thrust -= transform.forward * playerProps.thrustForce;
                    updateFacing = false;
                }
                else if (wasInReverse && velocity.magnitude < velocityThreshold)
                {
                    velocity *= 0;

                    //reset out facing
                    Debug.Log("flip reverse to forward");
                    transform.Rotate(0, 180, 0);

                    updateFacing = false;
                    wasInReverse = false;                    
                    wasInForward = true;

                    //forward on the ground means thrust on the forward vector
                    thrust += transform.forward * playerProps.thrustForce;

                    //give it a nudge
                    velocity = transform.forward * 0.51f;

                }
                else
                {
                    //forward on the ground means thrust on the forward vector
                    thrust += transform.forward * playerProps.thrustForce;
                    updateFacing = true;
                }

                playerProps.energy -= playerProps.consumption * Time.deltaTime;

                
            }
            else if (controller.backward)
            {

                isRotatingTurret = false;
                isInForward = false;
                isInReverse = true;

                if (wasInForward && velocity.magnitude >= velocityThreshold)
                {
                    //apply a brakeing force until we cross the zero threshold
                    updateFacing = false;
                    thrust -= transform.forward * playerProps.thrustForce;
                }
                else if (wasInForward && velocity.magnitude < velocityThreshold)
                {
                    velocity *= 0;

                    //set our facing to inverse
                    transform.Rotate(0, 180, 0);

                    updateFacing = false;
                    wasInReverse = true;
                    wasInForward = false;

                    //forward on the ground means thrust on the forward vector
                    thrust += transform.forward * playerProps.thrustForce;
                    //give it a nudge
                    velocity = transform.forward * 0.51f;

                }
                else if (!wasInReverse)
                {
                    //set our facing to inverse
                    transform.Rotate(0, 180, 0);
                    //give it a nudge
                    velocity = transform.forward * 0.51f;

                    updateFacing = false;
                    wasInReverse = true;

                }
                else
                {
                    //forward on the ground means thrust on the forward vector
                    thrust += transform.forward * playerProps.thrustForce;
                    updateFacing = true;
                }

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
                if (!controller.forward && !controller.backward && !isStrafeing && !wasStrafeing)
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
                    wasStrafeRight = false;
                }

                if (isStrafeing && !wasStrafeing)
                {
                    wasStrafeing = true;
                }
                if (isStrafeing)
                {
                    wasStrafeLeft = true;
                    updateFacing = false;
                }

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
                    wasStrafeLeft = false;                   
                }

                if (isStrafeing && !wasStrafeing)
                {                                        
                    wasStrafeing = true;                    
                }
                if (isStrafeing)
                {
                    wasStrafeRight = true;
                    updateFacing = false;
                }
            }

        }

        //if no key is pressed, add extra stopping force by diminishing velocity
        if (!controller.controlerOn)
            velocity *= playerProps.stoppingForce;

        //finally, if we are at a fraction of velocity threshold, we might as well stop
        if (velocity.magnitude < velocityThreshold * 0.5f && !controller.controlerOn)
        {

            if (wasInReverse)
            {
                //set our facing to inverse
                transform.Rotate(0, 180, 0);
                updateFacing = false;
                wasInReverse = false;

            }

            velocity *= 0;

            //clear all movement flags to be sure
            isStrafeing = false;
            isInReverse = false;
            isInForward = false;
            wasInForward = false;
            wasStrafeing = false;
            wasInReverse = false;
            wasStrafeLeft = false;
            wasStrafeRight = false;
            isRotatingTurret = false;
            updateFacing = false;
        }

        if (velocity.magnitude < velocityThreshold * 2.0f)
            updateFacing = false;        
            
        if (velocity.magnitude > velocityThreshold && wasStrafeing)
            updateFacing = false;
        else
            wasStrafeing = false;
    }
}
