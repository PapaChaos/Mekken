using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPhysicsTreads : playerPhysics
{

    public override void handleOnSurface()
    {
        //do we have any gas in the tank?
        if (playerProps.energy > 0.0f)
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

               
            }




            if (controller.forward)
            {
                isRotatingTurret = false;


                //if we are in reverse but swithc to forward, approach the forward threshold
                //by applying a negative forward force
                if (engagedReverse && velocity.magnitude >= velocityThreshold)
                {
                    thrust -= transform.forward * playerProps.thrustForce;

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
                if (!engagedReverse && velocity.magnitude >= velocityThreshold)
                {
                    thrust -= transform.forward * playerProps.thrustForce;
                }
                else if (!engagedReverse && velocity.magnitude < velocityThreshold)
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
                        
            turnForce *= (velocity.magnitude + 1);


            if (controller.left)
            {
                //handle stationary turn/targeting
                if (!controller.forward && !controller.backward )
                {
                    //sit and spin (better to handle inside player motion, easier to handle right here)
                    thrust *= 0;
                    playerGeometry.parent.Rotate(0, -Time.deltaTime * playerProps.rotateTurretFactor, 0);
                    isRotatingTurret = true;

                }
                else
                    thrust -= transform.right * turnForce;


                playerProps.energy -= playerProps.consumption * Time.deltaTime;

            }

            if (controller.right)
            {
                //handle stationary turn/targeting
                if (!controller.forward && !controller.backward )
                {
                    //sit and spin (better to handle inside player motion, easier to handle right here)
                    thrust *= 0;
                    playerGeometry.parent.Rotate(0, Time.deltaTime * playerProps.rotateTurretFactor, 0);
                    isRotatingTurret = true;


                }
                else
                    thrust += transform.right * turnForce;

                playerProps.energy -= playerProps.consumption * Time.deltaTime;

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
            isInReverse = false;
            engagedReverse = false;
            isRotatingTurret = false;

        }

    }

}
