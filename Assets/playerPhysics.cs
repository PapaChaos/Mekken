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
    public landerProperties landerProps;

    public bool isInReverse = false;    // as velocity determines our forward facing, when in reverse 
                                        // we need to inform movement to maintain the opposite facing


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //reset thrust each frame
        thrust.Set(0, 0, 0);

        //check to see if we have crossed the zero threshold for switching from reverse back to forward
        if (isInReverse && velocity.magnitude < 0.001f)
            isInReverse = false;
        
        //read controller values and set physics thrust
        if (landerProps.energy > 0.0f && !landerProps.onSurface)
        {
            //forward when in air means vertical thruster            
            if (controller.forward)
            {
                thrust.y = landerProps.thrustForce;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;

            }
            if (controller.left && landerProps.energy > 0.0f)
            {
                thrust += transform.right * -landerProps.lateralThrustForce;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;

            }
            if (controller.right && landerProps.energy > 0.0f)
            {
                thrust += transform.right * landerProps.lateralThrustForce;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;
            }
        }

        if (landerProps.energy > 0.0f && landerProps.onSurface)
        {
            if (controller.forward)
            {
                //forward on the ground means thrust on the forward vector
                thrust += transform.forward * landerProps.thrustForce;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;

            }
            if (controller.backward)
            {
                //this is a bit different in that it is always relational to 
                //the vehicle itself, not the forward vector.
                isInReverse = true; 
                thrust -= transform.forward * landerProps.lateralThrustForce;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;


            }
            if (controller.left)
            {
                thrust -= transform.right * landerProps.lateralThrustForce * (velocity.magnitude + 1) ;
                landerProps.energy -= landerProps.consumption * Time.deltaTime;


            }
            if (controller.right)
            {
                thrust += transform.right * landerProps.lateralThrustForce * (velocity.magnitude + 1);
                landerProps.energy -= landerProps.consumption * Time.deltaTime;
            }

        }
    }
}
