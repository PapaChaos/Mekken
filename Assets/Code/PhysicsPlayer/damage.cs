using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{

    public playerProperties playerProps;
    public playerMotion playerMove;
    public playerPhysics physicsController;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Awake()
    {
		gameManager = FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {

        if (playerProps.structuralIntegrity <= 0 && gameManager.getGameOver() == false)
        {
            Debug.Log("PLAYER DEAD BY DAMAGE");
            gameManager.setGameOver(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //TODO: best place to put this? maybe...

        //maybe we are stuck until poweroff?
        if (other.tag == "electric_fence")
        {
            Debug.Log("player damage script collided with" + other.name);

            //do something
            if (other.transform.GetComponent<Electrical_Trap>().Power == true)
            {
                transform.GetComponent<playerPhysics>().velocity *= 0;
            }

        }
        else if (other.tag == "Obstacle")
        {
            Debug.Log("Hit Something else " + other.name);


            //we hit an obstacle so we should bounce off of it using PlayerMotion
            //applyImpulseForce

            //get some info about current physics state, like ummm velocity
            Vector3 velo = physicsController.velocity;

            //REFINE
            physicsController.velocity *= 0;

            playerMove.hitSomething(other);

            //apply a force to the player
            playerMove.applyImpulseForce(velo * -10.0f);

           
        
        }
        

    }
    private void OnTriggerStay(Collider other)
    {

    }
    public void doDamage(float howMuch)
    {

        //TODO: display some damage
        Debug.Log(transform.name + " took " + howMuch + " damage");
        playerProps.structuralIntegrity -= howMuch;


    }


}
