using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderLegAnimation : MonoBehaviour
{

    public float amplitudeX = 1.0f;
    public float frequencyX = 1.0f;

    public float amplitudeY = 1.0f;
    public float frequencyY = 1.0f;

    public float amplitudeZ = 1.0f;
    public float frequencyZ = 1.0f;

    public Vector3 pos = new Vector3(0, 0, 0);

    public float moveX = 0;
    public float initX = 0;

    public float moveY = 0;
    public float initY = 0;

    public float moveZ = 0;
    public float initZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        initX = transform.localPosition.x;
        initY = transform.localPosition.y;
        initZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {

        moveX = Mathf.Sin(Time.time * frequencyX) * amplitudeX;
        moveY = Mathf.Sin(Time.time * frequencyY) * amplitudeY;
        moveZ = Mathf.Sin(Time.time * frequencyZ) * amplitudeZ;

        pos.Set(initX + moveX, initY + moveY, initZ + moveZ  );

        transform.localPosition = pos;
    }
}
