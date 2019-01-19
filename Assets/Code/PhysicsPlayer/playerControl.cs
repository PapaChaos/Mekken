using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    //made this a totally dumb script, just posting the direction the player
    //wants to move based on input device, thus de-coupling device from actual movement
    //TODO: use native unity Input.Axis mapping

    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;
    public bool jump = false;
    public bool strafe = false;
    public bool firePrimary = false;
    public bool fireSecondary = false;

    //is any control key pressed this frame (other than fire)?
    public bool controlerOn = false;

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
        strafe = false;
        firePrimary = false;
        fireSecondary = false;

        controlerOn = false;


        //assuming player 1
        //TODO: handle 2 players
        if (Input.GetKey(KeyCode.W) )
        {
            forward = true;
            controlerOn = true;
        }
        if (Input.GetKey(KeyCode.A) )
        {
            left = true;
            controlerOn = true;
        }
        if (Input.GetKey(KeyCode.D) )
        {
            right = true;
            controlerOn = true;
        }
        if (Input.GetKey(KeyCode.S) )
        {
            backward = true;
            controlerOn = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))  //combo key
        {            
            strafe = true;           
        }
    }
}
