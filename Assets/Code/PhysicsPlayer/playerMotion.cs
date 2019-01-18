using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotion : MonoBehaviour
{

    public playerProperties playerProps;
    public GameManager gameManager;
    public playerPhysics physics;

    //representation of the player, de-coupled from the empty doing the movement
    public Transform playerGeometry;
    public Vector3 facingVector =  new Vector3(0, 180, 0);
   

    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManager.gameOver)
        {
            
            if (!playerProps.onSurface)
            {
                handleMovementLanding();
                handleLanding();                
            }
            else
            {
                handleMovementSurface();                
            }
        }


    }

    void handleMovementSurface()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);

        finalForce += physics.thrust;
        //add more forces here

        physics.acceleration = finalForce / playerProps.mass;

        physics.velocity += physics.acceleration * Time.deltaTime;

        //move the player
        transform.position += physics.velocity * Time.deltaTime;


        //decay velocity if not on the ground due to friction
        physics.velocity *= playerProps.surface.surfaceFriction;

        Vector3 dir = physics.velocity;
        dir.Normalize();

        //look at the direction I am going
        transform.LookAt(transform.position + dir);

        //handle the facing of the geometry representing the player
        handleAvatarFacing();
       
        
    }

    void handleAvatarFacing()
    {

        //flip it when needed
        playerGeometry.localRotation = Quaternion.identity;
        if (physics.isInReverse)
        {
            playerGeometry.Rotate(facingVector);
        }

    }

    void handleLanding()
    {

        if (transform.position.y < playerProps.surface.landingHeight)
        {
            if (physics.velocity.magnitude > playerProps.structuralIntegrity * playerProps.integrityVelocity)
            {
                Debug.Log("CRASH!!!!!");
                gameManager.gameOver = true;
            }
            else
            {

                //distance from landing pad for a good landing
                float padsize = playerProps.surface.landingPad.localScale.x - 1.0f;

                if (Vector3.Magnitude(transform.position - playerProps.surface.landingPad.position) < padsize)
                {
                    Debug.Log("YOU MADE IT!!!");
                    playerProps.onSurface = true;
                    playerProps.energy = 200;
                    physics.acceleration *= 0;
                    physics.velocity *= 0;
                }
                else
                {
                    Debug.Log("MISSED THE PAD");
                    gameManager.gameOver = true;
                }
            }

        }

    }

    void handleMovementLanding()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, playerProps.surface.GRAVITY_CONSTANT * playerProps.mass, 0);
        finalForce += physics.thrust;


        physics.acceleration = finalForce / playerProps.mass;

        physics.velocity += physics.acceleration * Time.deltaTime;

        //move the player
        transform.position += physics.velocity * Time.deltaTime;

        //reset thrust
        physics.thrust.Set(0, 0, 0);



    }




}
