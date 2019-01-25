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

    public Quaternion accumRotation = Quaternion.identity;

    public FKsegment parent = null;
    public FKsegment child = null;

    public Vector3 Apos = Vector3.zero;
    public Vector3 Bpos = Vector3.zero;

    public FKSystem fkSystem;


    // Use this for initialization
    void Awake ()
    {

        //calculate length based on model imported
        if (child)
            length = Vector3.Distance(transform.position , child.transform.position);
        else
            length = 1.0f;// fine for FK, length does not matter for end effector

        accumRotation = transform.rotation;

        if (parent)
        {

            transform.rotation *= Quaternion.Inverse(parent.transform.rotation);
            accumRotation = transform.rotation;


        }
            
    }
	
	// Update is called once per frame
	void Update ()
    {

        float xRot = 0;

        
        if (jointEnabled)
        {
            xRot = Mathf.Sin(fkSystem.FKTime * frequency) * amplitude;
        }

        accumRotation *= Quaternion.EulerRotation(xRot * Mathf.Deg2Rad, 0,0);

        //Quaternion quat = new Quaternion();
        //quat.eulerAngles = accumRotation;

        if (parent)
        {
            transform.rotation = accumRotation * parent.transform.rotation;
        }
        else
        {
            transform.rotation = accumRotation;
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
