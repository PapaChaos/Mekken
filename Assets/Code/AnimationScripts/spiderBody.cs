using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderBody : MonoBehaviour
{
    // we use this to choose the animation update to apply - this refers to the person who made it
    // and/or the animation state we are in (idle,walk,run,die, etc...) 
    public enum WHOSE_UPDATE
    {
        JOHN,
        ADD_YOUR_NAME,
        ADD_THE_NEXT_PERSONS_NAME //etc...
    };

    public WHOSE_UPDATE whose = WHOSE_UPDATE.JOHN;

    public float groundOffset = 2.0f; //this too can be animated
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    public Vector3 animPosition = new Vector3(0,0,0);

    public playerProperties playerProps;

    private Vector3 initialPosition = new Vector3(0, 0, 0);
    private float myTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
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
        }
    }

    void updateJohn()
    {

        //simple up/down bob script with terrain following
        float animHeight = Mathf.Sin(myTime * frequency) * amplitude;

                
        animPosition = transform.position;
        
        animPosition.y = playerProps.terrainYPoint + groundOffset + animHeight;
        
        transform.position = animPosition;

        myTime += Time.deltaTime;


    }
}
