using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public bool inAir = false;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 finalForce;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            handleMovementAir();            
        }
        
    }


    void handleMovementAir()
    {
        //reset final force to the initial force of gravity

        finalForce.Set(0, -9.8f, 0);

        acceleration = finalForce;

        velocity += acceleration * Time.deltaTime;

        //move the player
        transform.position += velocity * Time.deltaTime;

        if (transform.position.y < 0.5f)
        {
            Debug.Log("BOOM!!!");
            inAir = false;
        }


    }
}
