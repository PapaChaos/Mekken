using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{


    landerProperties landerProps;


    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector
    

    // Start is called before the first frame update
    void Start()
    {

        landerProps = transform.GetComponent<landerProperties>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && landerProps.energy > 0.0f && !landerProps.onSurface)
        {
            thrust.y = landerProps.thrustForce;
            landerProps.energy -= landerProps.consumption * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A) && landerProps.energy > 0.0f)
        {
            thrust.x = -landerProps.lateralThrustForce;
            landerProps.energy -= landerProps.consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D) && landerProps.energy > 0.0f)
        {
            thrust.x = landerProps.lateralThrustForce;
            landerProps.energy -= landerProps.consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.W) && landerProps.energy > 0.0f && landerProps.onSurface)
        {
            thrust.z = landerProps.lateralThrustForce;
            landerProps.energy -= landerProps.consumption * 0.25f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S) && landerProps.energy > 0.0f && landerProps.onSurface)
        {
            thrust.z = -landerProps.lateralThrustForce;
            landerProps.energy -= landerProps.consumption * 0.25f * Time.deltaTime;

        }
    }
}
