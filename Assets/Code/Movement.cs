using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float GRAVITY_CONSTANT = -1.6f; //-9.8f;   // -1.6 for moon

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector

    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame


    //landing pad
    public Transform landingPad = null;
    public float landingHeight = 0.55f;

    //gameplay parameters
    public float mass = 1.0f;
    public float energy = 1.0f;
    public float consumption = 0.25f;
    public float structuralIntegrity = 1.0f;
    public float thrustForce = 5.0f;
    public float lateralThrustForce = 5.0f;
    public float surfaceFriction = 0.99f;

    public bool gameOver = false;
    public bool onSurface = false;

    public float integrityVelocity = 3.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameOver)
        {
            handleInput();
            
            if (!onSurface)
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
    void handleInput()
    {
        if (Input.GetKey(KeyCode.W) && energy > 0.0f && !onSurface)
        {
            thrust.y = thrustForce;
            energy -= consumption * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A) && energy > 0.0f)
        {
            thrust.x = -lateralThrustForce;
            energy -= consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D) && energy > 0.0f)
        {
            thrust.x = lateralThrustForce;
            energy -= consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.W) && energy > 0.0f && onSurface)
        {
            thrust.z = lateralThrustForce;
            energy -= consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S) && energy > 0.0f && onSurface)
        {
            thrust.z = -lateralThrustForce;
            energy -= consumption * 0.25f * Time.deltaTime;

        }
    }

    void handleMovementSurface()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, 0, 0);

        finalForce += thrust;
        //add more forces here

        acceleration = finalForce / mass;

        velocity += acceleration * Time.deltaTime;

        //move the player
        transform.position += velocity * Time.deltaTime;

        //decay velocity if not on the ground due to friction
        if (transform.position.y < landingHeight)
            velocity *= surfaceFriction ;

        //reset thrust
        thrust.Set(0, 0, 0);


    }

    void handleLanding()
    {

        if(transform.position.y < landingHeight)
        {
            if (velocity.magnitude > structuralIntegrity * integrityVelocity)
            {
                Debug.Log("CRASH!!!!!");
                gameOver = true;
            }
            else
            {

                //distance from landing pad for a good landing
                float padsize = landingPad.localScale.x - 1.0f;

                if (Vector3.Magnitude(transform.position - landingPad.position) < padsize)
                {
                    Debug.Log("YOU MADE IT!!!");
                    onSurface = true;
                    energy = 200;
                }
                else
                {
                    Debug.Log("MISSED THE PAD");
                    gameOver = true;
                }
            }

        }

    }

    void handleMovementLanding()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
        finalForce += thrust;


        acceleration = finalForce / mass;

        velocity += acceleration * Time.deltaTime;

        //move the player
        transform.position += velocity * Time.deltaTime;

        //reset thrust
        thrust.Set(0, 0, 0);



    }
    



}
