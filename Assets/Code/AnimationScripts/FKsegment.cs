using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKsegment : MonoBehaviour
{

    public float length = 2;
    public float rate = 10;
    public int direction = 1;

    public Vector3 constrainRotation = new Vector3(45, 45, 45);
    public Vector3 accumRotation = Vector3.zero;

    public FKsegment parent = null;
    public FKsegment child = null;

    public Vector3 Apos = Vector3.zero;
    public Vector3 Bpos = Vector3.zero;

    // Use this for initialization
    void Start ()
    {


    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 angles = Vector3.zero;

        angles.y = rate * Time.deltaTime * direction;

        accumRotation += angles;
        
        if (Mathf.Abs ( accumRotation.y ) > constrainRotation.y)
            direction *= -1;

        Quaternion quat = new Quaternion();
        quat.eulerAngles = accumRotation;

        if (parent)
        {
            transform.rotation = quat * parent.transform.rotation;
        }
        else
        {
            transform.rotation = quat;
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
