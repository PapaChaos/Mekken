using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use This On wanted Player Camera.

public class FollowCamera : MonoBehaviour
{


    public Transform PlayerTransform;

    private Vector3 CameraOffset;

    [Range(0.01f, 1.0f)]
    public float Smooth = 0.5f;

    void Start()
    {
        CameraOffset = transform.position - PlayerTransform.position;
    }

    void Update()
    {
        Vector3 NewPosition = PlayerTransform.position + CameraOffset;
  
        transform.position = Vector3.Slerp(transform.position, NewPosition, Smooth);
    }
}
