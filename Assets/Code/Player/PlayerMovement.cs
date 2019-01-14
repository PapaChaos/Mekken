using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	//References
	public Player player;

	//Movement Variables
	public bool canStrafe;
	public float forwardSpeed, strafeSpeed, rotationSpeed;

	//Adjustments
	public float heightAdjustment;

	//private variables
	private int playerNumber;
	private float xMov = 0, yMov = 0;

	private void Start()
	{
		playerNumber = player.PlayerNumber;
	}
	void Update () {
			if (canStrafe)
				xMov = strafeSpeed * Input.GetAxis("Horizontal Player " + (playerNumber+1));
			else
				transform.parent.Rotate(Vector3.up * (Input.GetAxis("Horizontal Player " + (playerNumber + 1)) *rotationSpeed), Space.World);

			yMov = forwardSpeed * Input.GetAxis("Vertical Player " + (playerNumber + 1));
			transform.parent.Translate((gameObject.transform.forward * yMov)+gameObject.transform.right * xMov, Space.World);

			transform.parent.Rotate(Vector3.up*(Input.GetAxis("Rotate Player " + (playerNumber + 1)) *rotationSpeed), Space.World);
	}
}
