using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProperties : MonoBehaviour
{
	public int playerNumber = 0;
	public bool lost = false;
	public GameManager gameManager;


	//gameplay parameters
	public float mass = 1.0f;
    public float energy = 100.0f;
    public float consumption = 0.25f;
    public float maxSpeed = 5.0f;

    public float structuralIntegrity = 1.0f;
    public float integrityVelocity = 3.0f;      //factor for velocity impact on integrity


    public float thrustForce = 5.0f;
    public float stoppingForce = 0.5f;

    //movement modifiers to force
    public float lateralFactor = 1.0f;  //turn factor is our turning "traction" value
    public float strafeFactor = 2.0f;   //how fast we move in strafe
    public float rotateTurretFactor = 200.0f; //TODO: decide if aiming is independent of driving driving direction

    //surface i am on
    public Transform surfaceTransform;
    public surfaceProperties surface;

    public bool onSurface = false;
    public float surfaceTraction = 1.0f;  //factor affecting friction based on locomotion type
    public float distanceOffGround = 1000.0f;
    public float surfaceOffset = 1.0f;
    public Vector3 hitTestMargin = new Vector3(0, 20, 0);

    public float terrainYPoint = 0;

    public bool isOnDeathTrap = false;
    public Vector3 surfaceNormal = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Awake()
    {
		if (!gameManager)
			gameManager = FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void Update()
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
        didHit = Physics.Raycast(transform.position + hitTestMargin, Vector3.down, out hit, 1000.0f, layerMask);

        if (didHit)
        {
            //get current distance off ground
            distanceOffGround = hit.distance - hitTestMargin.y;
            terrainYPoint = hit.point.y;
            surfaceNormal = hit.normal;

            if (distanceOffGround > surfaceOffset * 2.0f)
            {
                onSurface = false;
                
            }
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

                isOnDeathTrap = surface.deathTrap;
                Debug.Log(surfaceTransform.tag);
            }
        }
        else
        {
            //TODO: handle this!!
            Debug.Log("FELL OFF THE SURFACE!!!");
            //clear on surface
            onSurface = false;
        }
    }

	public void playerLost()
	{
		bool doneOnce = false;
		if (!doneOnce)
		{
			lost = true;
			doneOnce = true;
			gameManager.CheckRemainingPlayers();
		}
	}
}
