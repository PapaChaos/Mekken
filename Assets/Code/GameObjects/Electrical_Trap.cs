using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrical_Trap : MonoBehaviour
{ 

    public bool Power = true;
    private float timer = -1.0f;   //keep track of time elapsed

    public float interval = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //get time now and add some random
        timer = Time.time + Random.Range(0, 3);    
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //check if time elapsed is greater than time now
        if(Time.time > timer)
        {
            Power = !Power;
            timer = Time.time + interval + Random.Range(0, 3);

            Debug.Log("Power is " + Power);

        }


    }


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("electrical fence collided with " + other.transform.name);

        //TODO: add electrical zap here

        if (Power == false)
        {
            Debug.Log("Do nothing, power is off");

        }
        else
        {

            //Take dmg as player is inbound//
            Debug.Log("AU");

            if (other.tag == "Player")
            {
                other.transform.GetComponent<damage>().doDamage(1.0f);
            }

            
    
        }
    }
    private void OnTriggerStay(Collider other)
    {


    }

}
