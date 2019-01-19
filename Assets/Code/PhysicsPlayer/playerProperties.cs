using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProperties : MonoBehaviour
{


    //gameplay parameters
    public float mass = 1.0f;
    public float energy = 100.0f;
    public float consumption = 0.25f;
    public float maxSpeed = 5.0f;

    public float structuralIntegrity = 1.0f;
    public float integrityVelocity = 3.0f;      //factor for velocity impact on integrity


    public float thrustForce = 5.0f;
    public float stoppingForce = 0.5f;

    //movement modifiers to force
    public float lateralFactor = 1.0f;  //turn factor is our turning "traction" value
    public float strafeFactor = 0.25f;  //we strafe much slower 
    
    
    //surface i am on
    public surfaceProperties surface;
    public bool onSurface = false;
    public float surfaceTraction = 1.0f;  //factor affecting friction based on locomotion type

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
