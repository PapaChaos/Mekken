using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motionSounds : MonoBehaviour
{


    public AudioSource moveSound;
    public playerPhysics playerphys;

    private float stopTimer = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 xzVelo = playerphys.velocity;
        xzVelo.y = 0;

        float velomag = xzVelo.magnitude;

        if (velomag > 0.5f)
        {
            if (moveSound)
            {
                if (moveSound.isPlaying == false)
                {

                    moveSound.Play();
                    moveSound.pitch = 1.0f;
                }

                //moveSound.pitch = velomag / 5.0f;

            }



        }        
        else if (moveSound)
        {
            if(stopTimer < 0)
                stopTimer = Time.time + 1.0f;

            if (Time.time > stopTimer)
            {
                moveSound.Stop();
                stopTimer = -1;
            }
            else
               moveSound.pitch *= (0.8f * Time.deltaTime); 

        }
        
    }
}
