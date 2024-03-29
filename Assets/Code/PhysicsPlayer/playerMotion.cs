﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotion : MonoBehaviour
{
    public playerProperties playerProps;
    public playerPhysics physicsController;
   
    //representation of the player, de-coupled from the empty doing the movement
    public Transform playerGeometry;

    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame
    public float yPosGround = 0;

    public float velocityThreshold = 0.5f;

    public Vector3 finalPosition = new Vector3(0, 0, 0);
   
    private Vector3 externalImpulseForce = new Vector3(0, 0, 0);

    private bool hitObstacle = false;
    private float hitTimer = -1;


    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0 && Time.time > hitTimer )
        {
            hitTimer = -1;
            hitObstacle = false;
        }

        if (!playerProps.gameManager.getGameOver() && !playerProps.lost)
        {
            handleGravity();
            
            if (playerProps.onSurface == false)
                handleLanding();

            if (playerProps.onSurface)
            {
                physicsController.velocity.y = 0;

                if (playerProps.isOnDeathTrap)
                {
                    Debug.Log("DEAD!!!");
					playerProps.playerLost();
					//gameManager.setGameOver(true);
					physicsController.velocity *= 0;
                    physicsController.acceleration *= 0;
                    //TODO: Do something dammit!
                }

                handleMovementSurface();

                //handle slopes
                handleTerrainSlope();
			}

			if (gameObject.transform.position.y < -5f) //just a safety in case the player some how bugs or flies out of a death zone.
				playerProps.playerLost();
		}
    }

    public void hitSomething(Collider other)
    {
        hitObstacle = true;
        hitTimer = Time.time + 1.0f; 
    }


    public virtual void handleMovementSurface()
    {
        //TODO: modify physics to handle ice,mud, etc.. as a function of surface properties (friction is all we have ATM)
        //      need to modify controllability, acceleration, and forward facing based on additional properties, OR simply
        //      tag the surface as "Ice" for example, and throw in some controller messyness.

        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);

        //if i did NOT hit something, apply the controller force.
        if (!hitObstacle)
            finalForce += physicsController.thrust;

        //add more forces here
        physicsController.acceleration = finalForce / playerProps.mass;

        physicsController.velocity += physicsController.acceleration * Time.deltaTime;

        //not faster than max speed as a function of surface friction        
        clampVelocity( playerProps.maxSpeed * playerProps.surface.surfaceFriction );

        //move the player
        transform.position += physicsController.velocity * Time.deltaTime;
        
        //decay velocity if not on the ground due to friction
        physicsController.velocity *= playerProps.surface.surfaceFriction * playerProps.surfaceTraction;

        Vector3 dir = physicsController.velocity;
        dir.Normalize();

        //look at the direction I am going (if not strafeing)
        if(physicsController.updateFacing && !hitObstacle)
        {
            transform.LookAt(transform.position + dir);
        }

        //handle the facing of the geometry representing the player
        //this will depend on movement type and context
        handleAvatarFacing();

        //apply external forces last
        physicsController.velocity += externalImpulseForce;

        externalImpulseForce *= 0;
    }

    public void handleAvatarFacing()
    {
        //flip it when needed
        playerGeometry.localRotation = Quaternion.identity;
        if (physicsController.wasInReverse)
        {
            playerGeometry.Rotate(0,180,0); 
        }
    }

    public void handleTerrainSlope()
    {
        //get the normal from the polygon we are currently standing on (playerProps is always updating this)
        Vector3 surfNorm = playerProps.surfaceNormal;

        //get avatar current rotation
        Quaternion quat1 = playerGeometry.rotation;

        //get current forward vector of geometry
        Vector3 fwd = playerGeometry.forward;

        Vector3 cross = Vector3.Cross(fwd, surfNorm);
        
        //get that new rotation
        //Quaternion quat2 = playerGeometry.rotation;

        //interpolate from q1 to q2
        //playerGeometry.rotation = Quaternion.Lerp(quat1, quat2, 0.5f);
    }

    void handleLanding()
    {

        if (playerProps.distanceOffGround < playerProps.surfaceOffset )
        {
            if (physicsController.velocity.magnitude > playerProps.structuralIntegrity * playerProps.integrityVelocity)
            {
                Debug.Log("CRASH!!!!!");
				playerProps.gameManager.setGameOver(true);
            }
            else
            {
                //Debug.Log("LANDED SAFELY");
                if (playerProps.onSurface == false)
                {
                    playerProps.onSurface = true;
                }
            }
        }
        else
            playerProps.onSurface = false;
    }

    void handleGravity()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, playerProps.surface.GRAVITY_CONSTANT * playerProps.mass, 0);

        //finalForce += physicsController.thrust;


        physicsController.acceleration = finalForce / playerProps.mass;

        physicsController.velocity += physicsController.acceleration * Time.deltaTime;

        clampVelocity(200); //just a guess at what terminal velocity might be

        //move the player
        transform.position += physicsController.velocity * Time.deltaTime;


        if (playerProps.distanceOffGround < playerProps.surfaceOffset)
        {
            
            yPosGround = playerProps.terrainYPoint + playerProps.surfaceOffset; 
            
            finalPosition.Set(transform.position.x, yPosGround, transform.position.z);

            transform.position = finalPosition;
        }

        
    }

    public void clampVelocity(float max)
    {
        //GENERAL RULE OF VELOCITY : don't let them go too fast!!!        
        float maxSpeedSquared = max * max;
        float velMagSquared = physicsController.velocity.magnitude * physicsController.velocity.magnitude;
        if (velMagSquared > maxSpeedSquared)
        {
            physicsController.velocity *= (max / physicsController.velocity.magnitude);
        }

    }

    public void applyImpulseForce(Vector3 howMuch)
    {
        externalImpulseForce += howMuch;

    }
}
