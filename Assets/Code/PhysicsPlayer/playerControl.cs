using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    //made this a totally dumb script, just posting the direction the player
    //wants to move based on input device, thus de-coupling device from actual movement

    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;
    public bool jump = false;
    public bool fire = false;

    // Start is called before the first frame update
    void Start()
    {

    
    }

    // Update is called once per frame
    void Update()
    {

        //clear them all
        forward = false;
        backward = false;
        left = false;
        right = false;
        jump = false;
        fire = false;
        
        if (Input.GetKey(KeyCode.W) )
        {
            forward = true;
        }
        if (Input.GetKey(KeyCode.A) )
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.D) )
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.S) )
        {
            backward = true;
        }
    }
}
