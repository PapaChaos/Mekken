using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrical_Trap : MonoBehaviour
{

    public bool Power = true;
    private float timer = -1.0f;   //keep track of time elapsed
    public float interval = 1.0f;
    public Renderer rend;
    public AudioSource Electricity01;


    // Start is called before the first frame update
    void Start()
    {
        //get time now and add some random
        timer = Time.time + Random.Range(0, 3);

        Electricity01 = GetComponent<AudioSource>();

        rend = GetComponent<MeshRenderer>();
        rend.enabled = Power;

    }

    // Update is called once per frame
    void Update()
    {

        //check if time elapsed is greater than time now
        if (Time.time > timer)
        {
            Power = !Power;
            timer = Time.time + interval + Random.Range(0, 3);
            rend.enabled = Power;

        }


    }

    
    private void OnPostRender()
    {
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (Power == false)
            {
                Debug.Log("Do nothing, power is off");

            }
            else
            {

                //Take dmg as player is inbound//
                Debug.Log(other.name + " says AU");
                
                //TODO: add electrical zap here
                if(Electricity01.isPlaying == false)
                    Electricity01.Play(0);

                other.transform.GetComponent<damage>().doDamage(Time.deltaTime);
                other.transform.GetComponent<playerPhysics>().velocity *= 0;


            }
        }

        //TODO: Elctric fence should explode projectiles as well

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {

            if (Power == false)
            {  

            }
            else
            {

                //Take dmg as player is inbound//
                Debug.Log(other.name + " continues to say AU");

                //TODO: add electrical zap here 
                if(!Electricity01.isPlaying)
                    Electricity01.Play(0);

                other.transform.GetComponent<damage>().doDamage(Time.deltaTime);
                other.transform.GetComponent<playerPhysics>().velocity *= 0;

            }
        }
    }

}
