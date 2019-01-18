using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProperties : MonoBehaviour
{


    //gameplay parameters
    public float mass = 1.0f;
    public float energy = 100.0f;
    public float consumption = 0.25f;
    public float structuralIntegrity = 1.0f;
    public float thrustForce = 5.0f;
    public float lateralThrustForce = 5.0f;    
    public bool onSurface = false;
    public float integrityVelocity = 3.0f;

    //surface i am on
    public surfaceProperties surface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
