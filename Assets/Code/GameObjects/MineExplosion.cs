using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{

    private AudioSource explosionSound;
    private ParticleSystem explosionParticle;
    private GameObject Mine;
    private bool isTriggered = false;
    public float damageValue = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        explosionSound = GetComponent<AudioSource>();
        explosionSound.Stop();

        explosionParticle = GetComponent<ParticleSystem>();
        explosionParticle.Stop();
    }

    // On Trigger
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        if (isTriggered == false)
        {
                explosionParticle.Play();
                explosionSound.Play();
                Mine = GameObject.Find("ExplodingMine");
                Mine.GetComponent<MeshRenderer>().enabled = false;
                isTriggered = true;
                Destroy(Mine, 2);

                if (other.transform.GetComponent<damage>())
                {
                    other.transform.GetComponent<damage>().doDamage(damageValue);
                }
        }
    }
}