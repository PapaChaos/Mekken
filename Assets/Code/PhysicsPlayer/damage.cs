using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{

    public float damageReceived = 0;

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
        

        //maybe we are stuck until poweroff?
        if (other.tag == "electric_fence")
        {
            Debug.Log("damage script collided with" + other.name);

            //do something
            if (other.transform.GetComponent<Electrical_Trap>().Power == true)
            {
                transform.GetComponent<playerPhysics>().velocity *= 0;
            }

        }
        

    }
    private void OnTriggerStay(Collider other)
    {

    }
    public void doDamage(float howMuch)
    {

        Debug.Log(transform.name + " took " + howMuch + " damage");
        damageReceived = howMuch;

    }


}
