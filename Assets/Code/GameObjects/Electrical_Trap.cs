using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrical_Trap : MonoBehaviour
{ 

    public bool Power = true;
    private float time = 0.0f;   //keep track of time elapsed

    // Start is called before the first frame update
    void Start()
    {
              
        
       
        //get time now

    }

    // Update is called once per frame
    void Update()
    {
        
        //check if time elapsed is greater than time on

        //if yes and power on, turn off power, reset time (get the time)

        //if yes and power off, turn on power, reset time (get the time)

        //otherwise, accumulate time

    }


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("collided with " + other.transform.name);

        if (Power == false)
        {
            Debug.Log("Do nothing, power is off");

        }
        else
        {

            //Take dmg as player is inbound//
            Debug.Log("AU");
    
        }
    }
}
