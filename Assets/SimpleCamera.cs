using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midPoint = (player1.position + player2.position) * 0.5f;

        float playerDistance = (player1.position - player2.position).magnitude * 0.5f;

        float aspect = Camera.main.aspect;

        Vector3 perpendicular = Vector3.Cross( (player1.position - player2.position).normalized, Vector3.up);

        transform.position = midPoint + perpendicular * playerDistance * aspect + Vector3.up * playerDistance * aspect;

        transform.LookAt(midPoint);




    }
}
