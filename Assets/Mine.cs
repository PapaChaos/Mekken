using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Renderer rend;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    //Frasier, Katarina & Knut
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Start Explosion!!!");
            rend.enabled = false;
            explosionPrefab.SetActive(true);
            Invoke("DestroyMine", 2);
        }

    }

    void DestroyMine()
    {

        Destroy (gameObject);
    }

}
