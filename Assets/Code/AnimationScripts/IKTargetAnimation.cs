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
        SPECIFIC_POSITION,
        ADD_THE_NEXT_PERSONS_NAME //etc...
    };

    public IKsegement endEffector;
    public WHOSE_UPDATE whose = WHOSE_UPDATE.JOHN;

    //variables to handle individual surface following
    public float distanceOffGround = 0;
    public float terrainYPoint = 50;            //ensure first frame is off the ground
    public Vector3 surfaceNormal = new Vector3(0, 0, 0);
    public Vector3 hitTestMargin = new Vector3(0, 20, 0);  // how much extra height to use when raycasting down
    //surface i am on
    public Transform surfaceTransform;
    public surfaceProperties surface;
    public float groundOffset = 0.0f; //this too can be animated. by default the ball will hug the surface

    //JOHN's parameters and variables (you can use these too):
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    public Vector3 animPosition = new Vector3(0, 0, 0);

    //ADD_YOUR_NAME's parameters and variables:
    //etc...

    // Start is called when the game loads for the first time
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        //find out the Y position of the surface, so the ball hugs it.
        findSurfaceHeight();

        //and here is where we actually animate it. we use a switch statement to go to
        //the animation we want.

        switch (whose)
        {
            case WHOSE_UPDATE.JOHN:
            {
                updateJohn();
                break;
            }
            case WHOSE_UPDATE.SPECIFIC_POSITION:
            {
                updateSpecificPosition();
                break;
            }
        }

    }

    void findSurfaceHeight()
    {

        // Bit shift the index of the layer (9) to get a bit mask on surfaces
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 9.

        // But mayabe instead we want to collide against everything except layer 9. 
        // The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        //find out what surface I am on and swap it's surface properties into my component
        RaycastHit hit;
        bool didHit = false;
        didHit = Physics.Raycast(transform.position + hitTestMargin , Vector3.down, out hit, 1000.0f, layerMask);

        if (didHit)
        {


            //get current distance off ground
            distanceOffGround = hit.distance;
            terrainYPoint = hit.point.y;
            surfaceNormal = hit.normal;

    
            //change our surface properties if the surface changes
            if (hit.transform != surfaceTransform)
            {
                //GetComponent is an expensive call, so we only want to get it when it changes
                surfaceTransform = hit.transform;

                //check if this object in the level surface group has it's own surface properties
                surface = surfaceTransform.GetComponent<surfaceProperties>();

                //parent should have one. TODO: make an upward recursive search until we find a surface property component
                if (surface == null)
                    surface = surfaceTransform.GetComponentInParent<surfaceProperties>();


            }
        }
        else
        {
            //TODO: handle this??
            

        }

    }


    // we can add any number of "updates" and simply choose one above depending on state 
    void updateJohn()
    {
      

        //simple up/down bob script with terrain following
        float groundOffset = 1.0f + Mathf.Sin(Time.time * frequency) * amplitude;

        animPosition = transform.position;

        //follow the terrain
        animPosition.y = terrainYPoint + groundOffset;

        transform.position = animPosition;


    }

    void updateSpecificPosition()
    {



    }

    void setInitialPosition()
    {


        // we have to wait until the system calculates limb lengths before we can set initial positions
        if (endEffector.length <= 0)
            return;



        //we want to animate the ball which is the target of the leg. because the model
        //has been imported with parts in the "right" place, we can start by snapping 
        //the ball to the end effector location of the limb aka, the foot. I set this
        //up ahead of time in the inspector, so i can just grab it here, done and done.

        //initial ball position is foot position, plus it's forward faceing multiplied by it's length and a bit more
        transform.position = endEffector.transform.position + endEffector.transform.forward * endEffector.length * 1.1f;

        //this may or may not be the best approach to start. perhaps simply positioning in unity is better, it depends...
    }


}
