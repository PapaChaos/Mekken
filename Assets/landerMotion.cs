using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landerMotion : MonoBehaviour
{

    public landerProperties landerProps;
    public GameManager gameManager;
    public playerPhysics physics;

   
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
            
            if (!landerProps.onSurface)
            {
                handleMovementLanding();
                handleLanding();

                Debug.Log("in the air");
            }
            else
            {
                handleMovementSurface();
                Debug.Log("on the surface");
            }
        }


    }

    void handleMovementSurface()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);

        finalForce += physics.thrust;
        //add more forces here

        physics.acceleration = finalForce / landerProps.mass;

        physics.velocity += physics.acceleration * Time.deltaTime;

        //move the player
        transform.position += physics.velocity * Time.deltaTime;

        //decay velocity if not on the ground due to friction
        physics.velocity *= landerProps.surface.surfaceFriction;

        Vector3 dir = physics.velocity;
        dir.Normalize();
        if(physics.isInReverse && Mathf.Acos( Vector3.Dot( dir, transform.forward) ) > Mathf.PI/2 )
        {

            dir *= -1;
        }    
        
        transform.LookAt(transform.position + dir); 
        

    }

    void handleLanding()
    {

        if (transform.position.y < landerProps.surface.landingHeight)
        {
            if (physics.velocity.magnitude > landerProps.structuralIntegrity * landerProps.integrityVelocity)
            {
                Debug.Log("CRASH!!!!!");
                gameManager.gameOver = true;
            }
            else
            {

                //distance from landing pad for a good landing
                float padsize = landerProps.surface.landingPad.localScale.x - 1.0f;

                if (Vector3.Magnitude(transform.position - landerProps.surface.landingPad.position) < padsize)
                {
                    Debug.Log("YOU MADE IT!!!");
                    landerProps.onSurface = true;
                    landerProps.energy = 200;
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
        finalForce.Set(0, landerProps.surface.GRAVITY_CONSTANT * landerProps.mass, 0);
        finalForce += physics.thrust;


        physics.acceleration = finalForce / landerProps.mass;

        physics.velocity += physics.acceleration * Time.deltaTime;

        //move the player
        transform.position += physics.velocity * Time.deltaTime;

        //reset thrust
        physics.thrust.Set(0, 0, 0);



    }




}
