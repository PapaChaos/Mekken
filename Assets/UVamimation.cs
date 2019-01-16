using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVamimation : MonoBehaviour
{

    public Mesh mesh;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2[] uvs = mesh.uv;

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i].x += Time.deltaTime * speed; // Mathf.Sin(Time.time) * Time.deltaTime * 0.1f;
            //uvs[i].y += Mathf.Cos(Time.time) * Time.deltaTime * 0.1f;
        }

        //set uvs
        mesh.uv = uvs;



    }
}
