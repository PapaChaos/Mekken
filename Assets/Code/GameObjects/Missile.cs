using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{


    //these may be a function of the player, if they have control of cannon angle,
    //or it is an inherent property of the projectile/weapon system
    public Vector3 angle = new Vector3(0, 0.5f, 0);
    public float powerFactor = 20.0f;

    public bool inAir = false;
    public bool isInUse = false;

    //these we will eventual make private
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 finalForce;

    public float GRAVITY_CONSTANT = -9.8f;
    public float missileAccel = 0.0f;
    public float damageValue = 1.0f;

    public Transform owner;

    public ParticleSystem hitSystem;
    public ParticleSystem propulsionSystem;

    private AudioSource MissileHit;

    public float hitTimer = -1;


    // Start is called before the first frame update
    void Start()
    {

        inAir = false;
        isInUse = false;

        if (propulsionSystem)
            propulsionSystem.Stop();

        if (hitSystem)
            hitSystem.Stop();

        MissileHit = GetComponent<AudioSource>();
        if(MissileHit)
            MissileHit.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            handleMovementAir();
        }

        if (hitTimer > 0 && Time.time > hitTimer)
        {
            //reset timer
            hitTimer = -1;

            //send it to hell
            transform.position = new Vector3(0, -666, 0);

            //reshow
            transform.GetComponent<MeshRenderer>().enabled = true;

            //let it be used again
            isInUse = false;

        }

    }


    void handleMovementAir()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT, 0);
        finalForce += transform.forward * missileAccel;

        acceleration = finalForce;
        //add more forces here

        //append to velocity
        velocity += acceleration * Time.deltaTime;

        //point the missile
        transform.LookAt(transform.position + velocity);

        //move the object
        transform.position += velocity * Time.deltaTime;

    }

    public void fireMissile(Vector3 direction, float power)
    {

        velocity = (direction + angle) * power * powerFactor;
        inAir = true;
        isInUse = true;

        if (propulsionSystem)
            propulsionSystem.Play();

    }

    private void OnTriggerEnter(Collider other)
    {


        //TODO: make sure missile only collides with the enemy, not with who launched it,
        //      except where it is a grenade or a mortar gone very wrong. in that case,
        //      we prolly need a short timer after launch before it is "armed" so it
        //      clears our collision box before it can explode

        //do damage for other player only
        if (other.transform.GetComponent<damage>() && other.transform != owner)
        {

            Debug.Log("collided with player " + other.name);
            //make an explosion

            Debug.Log("BOOM!!!");
            inAir = false;

            if (hitSystem)
                hitSystem.Play();

            if (propulsionSystem)
                propulsionSystem.Stop();

            MissileHit.Play();

            hitTimer = Time.time + 2.0f;

            transform.GetComponent<MeshRenderer>().enabled = false;

            other.transform.GetComponent<damage>().doDamage(damageValue);

            //apply force as an impulse, this needs to be fairly huge as it applies to velocity only once.
            other.transform.GetComponent<playerMotion>().applyImpulseForce(transform.forward * damageValue * 10000.0f);



        }
        else if(other.transform != owner && other.tag!= "TargetCone" ) //it's not the owner and it's not the cone
        {
            //TODO: it hit something else, so explode? for grenades and mortars, we kinda want them to effect us too.
            //      this is gonna need more thinking to exclude certain things in the collision set

            //Debug.Log("collided with non-player " + other.name);

            /*
            Debug.Log("BOOM OTHER!!!");
            inAir = false;

            if (hitSystem)
                hitSystem.Play();

            if (propulsionSystem)
                propulsionSystem.Stop();

            MissileHit.Play();

            hitTimer = Time.time + 2.0f;

            transform.GetComponent<MeshRenderer>().enabled = false;
            */
        }

    }
}


