using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerConstructor : MonoBehaviour
{

    //TODO: this would need total re-write for multi-user, but we just hack it in for now


    bool isInitialized = false;
    public int playerNumber = 0;

    public PlayerMech mechConstruct;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {

            //get the mecha template assigned to me
            GameObject builder = GameObject.Find("PlayerMecha" + playerNumber);
            if (builder)
            {
                Debug.Log("found player " + playerNumber);
                mechConstruct = builder.GetComponent<PlayerMech>();
            }
            else
            {
                Debug.Log("cant find player " + playerNumber);
                return; //early out
            }

            

            //playerMotion contains player geometry, so grab that and use the "for each" loop to
            //decide what to destroy of the objects I don't need
            Transform pm = transform.GetComponent<playerMotion>().playerGeometry;


            //this would be a fine place for a bitmask! 
            //but lets just get the job done, this should work fine for now
            foreach (Transform child in transform)
            {

                //prolly want to apply final corrections and positions here as well

                bool nukeIt = true;

                //deal with body, we never nuke MechMainBodyA
                if (child.name == "MechMainBodyA")
                    nukeIt = false;

                //deal with the head first, one of three possible names
                if (mechConstruct.mechHead.name == child.name)
                    nukeIt = false;

                //now just use the bools
                if (child.name == "TankTreadsFrameA" && mechConstruct.mechTreads)
                    nukeIt = false;

                if (child.name == "AnimatedSpiderLegs" && mechConstruct.mechQuadruped)
                    nukeIt = false;

                if (child.name == "AnimatedBipedalLegs" && mechConstruct.mechBiped)
                    nukeIt = false;

                if (child.name == "RocketLauncherR" && mechConstruct.RocketLauncherRight)
                    nukeIt = false;

                if (child.name == "GrenadeLauncherR" && mechConstruct.GrenadeLauncherRight)
                    nukeIt = false;

                if (child.name == "MortarLauncherR" && mechConstruct.MortarLauncherRight)
                    nukeIt = false;

                if (child.name == "RocketLauncherL" && mechConstruct.RocketLauncherLeft)
                    nukeIt = false;

                if (child.name == "GrenadeLauncherL" && mechConstruct.GrenadeLauncherLeft)
                    nukeIt = false;

                if (child.name == "MortarLauncherL" && mechConstruct.MortarLauncherLeft)
                    nukeIt = false;

                //hmmm is this gonna screw the next child? do i need to mark it for destruction, then loop again??
                if (nukeIt)
                    Destroy(child);



            }
            

            //if i have a rocket launcher, set it's target now?

            //mark initialized

            //remove this script?
        }
    }
}
