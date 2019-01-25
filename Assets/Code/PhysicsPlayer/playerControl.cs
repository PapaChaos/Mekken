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
        /* <JPK> not needed and indeed controlerOn should be false
        if (Input.GetAxis("Horizontal Player 1") == 0)
        {
            right = false;
            left = false;

            controlerOn = true;
        }
        */
       
        if (Input.GetAxis("Horizontal Player 1") > 0.1)
        {
            right = true;
            //<JPK> not needed, all control booleans are cleared at the top
            //left = false;

            controlerOn = true;
        }
        if (Input.GetAxis("Horizontal Player 1") < -0.1)
        {
            left = true;
            //<JPK> not needed, all control booleans are cleared at the top
            //right = false;

            controlerOn = true;
        }

        //<JPK> not needed, all control booleans are cleared at the top
        /*
        if (Input.GetAxis("Vertical Player 1") == 0)
        {
            forward = false;
            backward = false;

            controlerOn = true;
        }
        */

        if (Input.GetAxis("Vertical Player 1") > 0.1f)
        {
            forward = true;
            //backward = false;

            controlerOn = true;
        }
        if (Input.GetAxis("Vertical Player 1") < -0.1f)
        {
            backward = true;
            //forward = false;

            controlerOn = true;
        }

        //TODO: rapid fire or single shot?
        if (Input.GetButton("Fire Player 1")) 
        {
            firePrimary = true;
        }

        //TODO: control for power on missle launch

    }
}
