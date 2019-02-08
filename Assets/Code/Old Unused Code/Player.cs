using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject MovementType, FrameType, WeaponRight, WeaponLeft, HeadType;

	public int PlayerNumber;

	private float adjustment_Movement;

	// Use this for initialization
	void Awake () {
		GameObject m_MovementType = Instantiate(MovementType, new Vector3(transform.position.x,transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
		m_MovementType.GetComponent<PlayerMovement>().player = this;
		adjustment_Movement = m_MovementType.GetComponent<PlayerMovement>().heightAdjustment;
		GameObject Frame = Instantiate(FrameType, new Vector3(transform.position.x, transform.position.y+ adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, transform.forward * 20 + transform.position);
	}
}
