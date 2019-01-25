using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKsegment : MonoBehaviour
{

    public float length = 2;

    public float amplitude = 1.0f;
    public float frequency = 1.0f;
   
    public bool jointEnabled = false;

    //you can use vector3 or quaternion to animate the rotations
    //as you see fit
    public Vector3 accumRotation = new Vector3(0,0,0);
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

        //just a simple rotation on the x axis
        float xRot = 0;



        if (jointEnabled)
        {

            //play with parameters to alter motion. 
            xRot = Mathf.Sin(fkSystem.FKTime * frequency) * amplitude;

            accumRotation.x += xRot;

            QaccumRotation *= Quaternion.Euler(xRot, 0, 0);

            quatFinal = QstartRotation * QaccumRotation;

       
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, quatFinal, Time.deltaTime);
    }

    

}
