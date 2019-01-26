using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetAnimation : MonoBehaviour
{
    // we use this to choose the animation update to apply - this refers to the person who made it
    // and/or the animation state we are in (idle,walk,run,die, etc...) 
    public enum WHOSE_UPDATE
    {
        JOHN,
        ADD_YOUR_NAME,
        ADD_THE_NEXT_PERSONS_NAME //etc...
    };

    public IKsegement endEffector;
    public WHOSE_UPDATE whose = WHOSE_UPDATE.JOHN;



    //JOHN's parameters and variables:
    private bool initialized = false;


    //ADD_YOUR_NAME's parameters and variables:
    //etc...

    // Start is called when the game loads for the first time
    void Start()
    {
        //assume at start we are not initialized
        //we have to wait until the system calculates limb lengths before we can set initial positions
        initialized = false;

    }

    // Update is called once per frame
    void Update()
    {

        //and here is where we actually animate it. we use a switch statement to go to
        //the animation we want.

        switch (whose)
        {
            case WHOSE_UPDATE.JOHN:
            {
                updateJohn();
                break;
            }
        }

    }




    // we can add any number of "updates" and simply choose one above depending on state 
    void updateJohn()
    {
        //this stinks that we constantly have to check this, but it works...
        if (!initialized)
            setInitialPosition();  //based on the model position of the limbs.


    }
    void setInitialPosition()
    {


        // we have to wait until the system calculates limb lengths before we can set initial positions
        if (endEffector.length <= 0)
            return;


        initialized = true;
        //we want to animate the ball which is the target of the leg. because the model
        //has been imported with parts in the "right" place, we can start by snapping 
        //the ball to the end effector location of the limb aka, the foot. I set this
        //up ahead of time in the inspector, so i can just grab it here, done and done.

        //initial ball position is foot position, plus it's forward faceing multiplied by it's length and a bit more
        transform.position = endEffector.transform.position + endEffector.transform.forward * endEffector.length * 1.1f;

        //this may or may not be the best approach to start. perhaps simply positioning in unity is better, it depends...
    }


}
