using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    //Katarina, Frasier & Knut

    private AudioSource explosionSound;
    public particle_Explosion explosionParticle;
    public float damageValue = 1.0f;

    private float timer = -1;

    // Start is called before the first frame update
    void Start()
    {
        explosionSound = GetComponent<AudioSource>();
        explosionSound.Stop();
        explosionParticle.transform.position = transform.position;

    }
    private void Update()
    {
        //mine has been discharged, but we want to wait a bit before we hide the explosion
        if (timer > 0 && Time.time > timer)
        {
            //reset the timer
            timer = -1;

            //send the mine to hell
            transform.position = new Vector3(0, -666, 0);

        }
    }

    // On Trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            explosionSound.Play();
            explosionParticle.PlaySystem();

            timer = Time.time + 3;

            if (other.transform.GetComponent<damage>())
            {
                other.transform.GetComponent<damage>().doDamage(damageValue);
            }

        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          //maybe something in here??
        }
    }
}