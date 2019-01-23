using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotion : MonoBehaviour
{

    public playerProperties playerProps;
    public GameManager gameManager;
    public playerPhysics physicsController;

    //representation of the player, de-coupled from the empty doing the movement
    public Transform playerGeometry;
    public Vector3 facingVector =  new Vector3(0, 180, 0);
   

    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame
    public float yPosGround = 0;


    public Vector3 finalPosition = new Vector3(0, 0, 0);
   
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManager.gameOver)
        {
            if (playerProps.onSurface == false)
            {
                handleGravity();
                handleLanding();
            }

            if (playerProps.onSurface)
                handleMovementSurface();

        }


    }

    public virtual void handleMovementSurface()
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
        clampVelocity( playerProps.maxSpeed * playerProps.surface.surfaceFriction );

        //move the player
        transform.position += physicsController.velocity * Time.deltaTime;
        
        //decay velocity if not on the ground due to friction
        physicsController.velocity *= playerProps.surface.surfaceFriction * playerProps.surfaceTraction;

        Vector3 dir = physicsController.velocity;
        dir.Normalize();

        //look at the direction I am going (if not strafeing)
        if(!physicsController.isStrafeing && 
           !physicsController.wasStrafeing && 
           !physicsController.isRotatingTurret && 
           physicsController.velocity.magnitude > 0)
        {
            transform.LookAt(transform.position + dir);
        }

        //handle the facing of the geometry representing the player
        //this will depend on movement type and context
        handleAvatarFacing();
       
        
    }

    public void handleAvatarFacing()
    {

        //flip it when needed
        playerGeometry.localRotation = Quaternion.identity;
        if (physicsController.engagedReverse)
        {
            playerGeometry.Rotate(facingVector);  //180 degrees on the Y
        }
        
    }

    void handleLanding()
    {

        if (playerProps.distanceOffGround < 1.0)
        {
            if (physicsController.velocity.magnitude > playerProps.structuralIntegrity * playerProps.integrityVelocity)
            {
                Debug.Log("CRASH!!!!!");
                gameManager.gameOver = true;
            }
            else
            {
                //Debug.Log("LANDED SAFELY");
                if (playerProps.onSurface == false)
                {
                    playerProps.onSurface = true;
                    physicsController.acceleration *= 0;
                    physicsController.velocity *= 0;
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
        finalForce += physicsController.thrust;


        physicsController.acceleration = finalForce / playerProps.mass;

        physicsController.velocity += physicsController.acceleration * Time.deltaTime;

        clampVelocity(200); //just a guess at what terminal velocity might be

        //move the player
        transform.position += physicsController.velocity * Time.deltaTime;


        if (playerProps.distanceOffGround < 1.0f)
        {
            
            yPosGround = playerProps.terrainYPoint + 1.0f; 
            
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


}
