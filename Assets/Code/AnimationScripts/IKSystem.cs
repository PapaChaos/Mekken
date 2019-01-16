using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour {


    public IKsegement[] segments;

    public int childcount = 0;

    public Transform target = null;

    public bool isReaching = false;
    public bool isDragging = false;

    private IKsegement lastSegment = null;
    private IKsegement firstSegment = null;



    // Use this for initialization
    void Start ()
    {

        //lets buffer our segments in an array
        childcount = transform.childCount;

        segments = new IKsegement[childcount];
        int i = 0;

        foreach (Transform child in transform)
        {
            segments[i] = child.GetComponent<IKsegement>();
            i++;
        }



        firstSegment = segments[0];
        lastSegment = segments[childcount - 1];


    }
	
	// Update is called once per frame
	void Update ()
    {

        if (isDragging)
        {

            lastSegment.drag(target.position);
            
        }
        else if (isReaching)
        {
            //call reach on the last
            lastSegment.reach(target.position);

            //and forward update on the first
            //we needed to maintain that first segment original position
            //which is the position of the IK system itself
            firstSegment.transform.position = transform.position;
            firstSegment.updateSegmentAndChildren();

        }
    }


}

