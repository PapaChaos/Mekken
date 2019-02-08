using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MekkenPhysics : MonoBehaviour {

	public float Mass; //kg
	public Vector3 Force, Acceleration, Velocity;
	public bool Disabled, Colliding;

	private Vector3 Gravity = new Vector3(0f, -9.8f, 0f); //maybe make this public in gamemode?
	private bool Grounded = false;
	private Rigidbody Rb;

	public List<BoxCollider> C_Boxes;
	public List<bool> C_Boxes_colliding;

	public List<List<Collider>> col;
	public Collisions cib = new Collisions();

	private void Awake()
	{


		Rb = gameObject.GetComponent<Rigidbody>();
		if (!Rb)
		{
			print(gameObject.name + " is missing a rigidbody.");
		}

		foreach (BoxCollider c in gameObject.GetComponentsInChildren<BoxCollider>() as BoxCollider[])
		{
			C_Boxes.Add(c);
		}
		Rb.useGravity = false;
		Rb.isKinematic = true;
		InvokeRepeating("CalculateForce", 0f, 0.01f);
	}

	void Update() {
		if (!Colliding)
		{
			Grounded = false;
		}
	}


	void CalculateForce()
	{
		if (Mass == 0)
		{
			print(gameObject.name + " is missing mass!");
		}
		Force = Mass * Acceleration;

		Colliding = false;

		C_Boxes_colliding.Clear();

		foreach (BoxCollider BC in C_Boxes) {
			C_Boxes_colliding.Add(cib.CollisionScan(BC));
			col.Add(cib.CollidersScan(BC));
			//CCollidersScan(BC);
		}

		foreach (bool b in C_Boxes_colliding)
			if (b)
				Colliding = true;


		if (!Colliding && Force.y > -40) Acceleration += Gravity*0.0001f;

		gameObject.transform.Translate(Force * 0.0001f);
	}

	public void PhysicsMovement(Vector3 Movement)
	{
		Acceleration.x = Movement.x;
		Acceleration.z = Movement.z;
		//return Movement; 
	}

	[System.Serializable]
	public class Collisions
	{
		public List<List<Collider>> CollisionsInBoxes;
		public List<Collider> CollisionsInBox;



		public bool CollisionScan(BoxCollider box)
		{
			Collider[] allOverlappingColliders = Physics.OverlapBox(box.center, box.size / 2);

			foreach (Collider col in allOverlappingColliders)
				return true;

			return false;
		}

		public List<Collider> CollidersScan(BoxCollider box)
		{
			Collider[] allOverlappingColliders = Physics.OverlapBox(box.center, box.size / 2);

			List<Collider> gencol = new List<Collider>();

			foreach (Collider c in allOverlappingColliders)
			{
				if (c != null)
					gencol.Add(c);
			}
			//CollisionsInBox = gencol;
			return gencol;
		}
	}
}
