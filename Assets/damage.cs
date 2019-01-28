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
        Debug.Log("damage script collided with" + other.name);     
        

    }

    public void doDamage(float howMuch)
    {

        Debug.Log(transform.name + " took " + howMuch + " damage");

    }


}
