using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKsegment : MonoBehaviour
{

    // we use this to choose the animation update to apply - this refers to the person who made it
    // and/or the animation state we are in (idle,walk,run,die, etc...) 
    public enum WHOSE_UPDATE
    {
        JOHN,
        ADD_YOUR_NAME,
        ADD_THE_NEXT_PERSONS_NAME,
        SOMEONE_ELSE//etc...
    };

    
    public WHOSE_UPDATE whose = WHOSE_UPDATE.JOHN;
    

    
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
   
    public bool jointEnabled = false;

    //you can use vector3 or quaternion to animate the rotations
    //as you see fit
    public Vector3 accumRotation = new Vector3(0, 0, 0);
    public Vector3 startRotation = new Vector3(0, 0, 0);

    public Quaternion QstartRotation = Quaternion.identity;
    public Quaternion QaccumRotation = Quaternion.identity;


    public FKSystem fkSystem;
    public Quaternion quatFinal = Quaternion.identity;


    // Use this for initialization
    void Awake ()
    {


        startRotation = transform.rotation.eulerAngles;
        QstartRotation = transform.rotation;
        




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
            case WHOSE_UPDATE.ADD_YOUR_NAME:
                {
                    //etc...

                    break;
                }
            case WHOSE_UPDATE.SOMEONE_ELSE:
                {
                    //etc...
                    updateSomeoneElse();
                    break;
                }


        }

        
    }

    void updateJohn()
    {
        //just a simple rotation on the x axis
        float xRot = 0;


        if (jointEnabled)
        {

            //play with parameters to alter motion. 
            xRot = Mathf.Sin(fkSystem.FKTime * frequency) * amplitude;


            //i'm supporting both euler and quat rotations, take yer pick
            accumRotation.x += xRot;

            QaccumRotation *= Quaternion.Euler(xRot, 0, 0);

            quatFinal = QstartRotation * QaccumRotation;


        }

        transform.rotation = quatFinal;

        //use this code instead if you are doing something with random numbers perlin noise, hint hint.
        //transform.rotation = Quaternion.Lerp(transform.rotation, quatFinal, Time.deltaTime);

    }

    void updateSomeoneElse()
    {
        //just a simple rotation on the x axis
        float xRot = 0;


        if (jointEnabled)
        {

            //play with parameters to alter motion. 
            xRot = Random.Range (-60, 60) + Mathf.Sin(fkSystem.FKTime * frequency) * amplitude;

            //i'm supporting both euler and quat rotations, take yer pick
            //accumRotation.x += xRot;

            QaccumRotation *= Quaternion.Euler(xRot, 0,  0 );

            quatFinal = QstartRotation * QaccumRotation;


        }

        //transform.rotation = quatFinal;

        //use this code instead if you are doing something with random numbers perlin noise, hint hint.
        transform.rotation = Quaternion.Lerp(transform.rotation, quatFinal, Time.deltaTime * 0.5f);

    }

}
