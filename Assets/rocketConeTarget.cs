using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketConeTarget : MonoBehaviour
{

    public bool lockedOn = false;
    public Transform me;
    public Transform lockedObj = null;
    
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

        if (other.tag == "Player" && other.transform != me && lockedObj == null)
        {
            lockedOn = true;
            lockedObj = other.transform;
        }

    }
    private void OnTriggerStay(Collider other)
    {
       
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.transform == lockedObj )
        {
            lockedOn = false;
            lockedObj = null;
        }


    }


}
