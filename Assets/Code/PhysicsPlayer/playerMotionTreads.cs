using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotionTreads : playerMotion
{


    public override void handleMovementSurface()
    {

        //TODO: modify physics to handle ice,mud, etc.. as a function of surface properties (friction is all we have ATM)
        //      need to modify controllability, acceleration, and forward facing based on additional properties, OR simply
        //      tag the surface as "Ice" for example, and throw in some controller messyness.

        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);

        finalForce += physicsController.thrust;
        //add more forces here

        physicsController.acceleration = finalForce / playerProps.mass;

        physicsController.velocity += physicsController.acceleration * Time.deltaTime;

        //not faster than max speed as a function of surface friction        
        clampVelocity(playerProps.maxSpeed * playerProps.surface.surfaceFriction);

        //move the player
        transform.position += physicsController.velocity * Time.deltaTime;

        //decay velocity if not on the ground due to friction
        physicsController.velocity *= playerProps.surface.surfaceFriction * playerProps.surfaceTraction;

        Vector3 dir = physicsController.velocity;
        dir.Normalize();

        //look at the direction I am going (if not strafeing)
        if ( !physicsController.isRotatingTurret &&
             physicsController.velocity.magnitude > velocityThreshold)
        {
            transform.LookAt(transform.position + dir);
        }

        //handle the facing of the geometry representing the player
        //this will depend on movement type and context
        handleAvatarFacing();


    }



}
