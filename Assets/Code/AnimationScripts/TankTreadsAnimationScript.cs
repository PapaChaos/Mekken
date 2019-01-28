using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTreadsAnimationScript : MonoBehaviour
{

    //<JPK> @EvenRodar couple things to add here. First speed should be a function of playerPhysics.velocity.magnitude
    //and indeed can be directly referenced through the playerPhysics component. Also if the tank is doing "Sit and Spin,"
    //one tread should go in one direction, the other tread in the opposite direction. I have made mesh and speed private
    //and added a reference to playerPhysics, which I assigned in the Inspector. See if you can finish it up.

    private Mesh mesh;              //the mesh to UV animate
    private float speed;            //the rate at which it animates
    private int direction = 1;      //should be 1 or -1 depending on movement state

    public playerPhysics physicsController;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

    }

    // Update is called once per frame
    void Update()
    {
        if (physicsController.isInForward)
        {
            direction = -1;
        }
        else
        {
            direction = 1;

        }

        //direction is a function of physicsController.isInForward , .isInReverse, and .isRotatingTurret

        //we need to know if we are left or right tread, our velocity, and movement type
        speed = direction * physicsController.velocity.magnitude * 0.1f; // multiplied by some scalar to make it look right

        Vector2[] uvs = mesh.uv;

        for (int i = 0; i < uvs.Length; i++)
        {
            
            uvs[i].y += Time.deltaTime * speed ; 
        }

        //Set uvs
        mesh.uv = uvs;



    }
}
