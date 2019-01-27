using UnityEngine;

public class PlayerMech : MonoBehaviour
{

	public GameObject mechMovement, mechFrame, mechWeaponRight, mechWeaponLeft, mechHead;

	public int PlayerNumber;

	private float adjustment_Movement;
	private void Awake()
	{

	}

	//This gets called from the player construction HUD.
	public void ConstructMecha()
	{
		GameObject m_MovementType = Instantiate(mechMovement, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
		//m_MovementType.GetComponent<PlayerMech>().player = this;
		//adjustment_Movement = m_MovementType.GetComponent<PlayerMovement>().heightAdjustment;

		GameObject Frame = Instantiate(mechFrame, new Vector3(transform.position.x, transform.position.y + adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);
	}
}
