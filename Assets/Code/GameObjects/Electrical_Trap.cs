using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrical_Trap : MonoBehaviour
{ 

    public bool Power = true;
    private float time = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

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
