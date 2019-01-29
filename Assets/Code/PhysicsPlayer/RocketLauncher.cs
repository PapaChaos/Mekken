using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{


    public Transform target;
    private Quaternion initRotation = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        initRotation = transform.rotation;
        transform.LookAt(target.position);

        transform.rotation = Quaternion.Lerp(initRotation, transform.rotation, Time.deltaTime * 10.0f);

    }
}
