using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{

    public playerProperties playerProps;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
