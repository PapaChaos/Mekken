using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKsegment : MonoBehaviour
{

    public float length = 2;

    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    public float phase = 1.0f;


    public bool jointEnabled = false;

    public Vector3 accumRotation = new Vector3(0,0,0);
    public Vector3 startRotation = new Vector3(0, 0, 0);

    public FKsegment parent = null;
    public FKsegment child = null;

    public Vector3 Apos = Vector3.zero;
    public Vector3 Bpos = Vector3.zero;

    public FKSystem fkSystem;
    public Quaternion quatFinal = Quaternion.identity;


    // Use this for initialization
    void Awake ()
    {

        //calculate length based on model imported
        if (child)
            length = Vector3.Distance(transform.position , child.transform.position);
        else
            length = 1.0f;// fine for FK, length does not matter for end effector

        startRotation = transform.rotation.eulerAngles;


            
    }
	
	// Update is called once per frame
	void Update ()
    {

        float xRot = 0;

        
        if (jointEnabled)
        {
            xRot = Mathf.Sin(fkSystem.FKTime * frequency) * amplitude;
        }

        accumRotation.x += xRot;

        
        if (parent)
        {
            quatFinal.eulerAngles = (startRotation + accumRotation + parent.transform.rotation.eulerAngles);
            transform.rotation = quatFinal;
        }
        else
        {
            quatFinal.eulerAngles = accumRotation;
            transform.rotation = quatFinal;
        }        

        updateSegmentAndChildren();
    }

    public void updateSegmentAndChildren()
    {

        updateSegment();

        //update its children
        if (child)
            child.updateSegmentAndChildren();



    }

    public void updateSegment()
    {

        if (parent)
        {

            Apos = parent.Bpos;         //could also use parent endpoint...
            transform.position = Apos;  //move me to Apos (parent endpoint)
        }
        else
        {
            //Apos is always my position
            Apos = transform.position;
        }

        //Bpos is always where the endpoint will be, as calculated from length 
        calculateBpos();
    }

    void calculateBpos()
    {
        Bpos = Apos + transform.forward * length;
    }
}
