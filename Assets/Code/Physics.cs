using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	public float Mass; //kg
	public Vector3 Force, Acceleration, Velocity;
	public bool disabled;

	//private Vector3 

	private Vector3 Gravity = new Vector3(0f, -9.8f, 0f); //maybe make this public in gamemode?
	private bool Grounded = false;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update() {
		if (!disabled)
		{
			Force = Mass * Acceleration;
			Force += Gravity;

			if(Mass != 0f)
			Acceleration = Force / Mass;

			Velocity += Acceleration * Time.deltaTime;

			transform.Translate(Velocity * Time.deltaTime);
		}
	}
}
