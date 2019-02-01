using UnityEngine;

public class PlayerMech : MonoBehaviour
{

	public GameObject mechMovement, mechFrame, mechWeaponRight, mechWeaponLeft, mechHead;

	public bool mechTreads = false;
	public bool mechQuadruped = false;
	public bool mechBiped = false;

	public bool RocketLauncherRight = false;
	public bool GrenadeLauncherRight = false;
	public bool MortarLauncherRight = false;

	public bool RocketLauncherLeft = false;
	public bool GrenadeLauncherLeft = false;
	public bool MortarLauncherLeft = false;

	public Vector3 GrenadeLauncherPosition = new Vector3(1.67f, 0f, 0.614f);
	public Vector3 RocketLauncherPosition = new Vector3(2.724f, 0f, 0f);
	public Vector3 MortarLauncherPosition = new Vector3(3.13f, 0f, 0f);

	public int PlayerNumber;

	private float adjustment_Movement;
	private void Awake()
	{
		GrenadeLauncherPosition = new Vector3(1.67f, 0f, 0.614f);
		RocketLauncherPosition = new Vector3(2.724f, 0f, 0f);
		MortarLauncherPosition = new Vector3(3.13f, 0f, 0f);
	}

	//This gets called from the player construction HUD.
	public void ConstructMecha(int playerNumber)
	{

        this.gameObject.name = "PlayerMecha" + playerNumber;

        PlayerNumber = playerNumber;

		//////////////////////////////
		//		Movement Module		//
		//////////////////////////////

		GameObject m_MovementType = Instantiate(mechMovement, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
		if (mechTreads)
		{
			m_MovementType.transform.localPosition = new Vector3(m_MovementType.transform.localPosition.x, 1.5f, m_MovementType.transform.localPosition.z);
		}
		//m_MovementType.GetComponent<PlayerMech>().player = this;
		//adjustment_Movement = m_MovementType.GetComponent<PlayerMovement>().heightAdjustment;



		//////////////////////////////
		//			Frame			//
		//////////////////////////////

		//Frame instantiate is not necessary on the other movement modules as they already included in the meshes and we have only one frame.
		if (mechTreads)
		{
			GameObject Frame = Instantiate(mechFrame, new Vector3(transform.position.x, transform.position.y + adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);
			Frame.transform.localPosition = new Vector3(m_MovementType.transform.localPosition.x, 1.745f, m_MovementType.transform.localPosition.z);

		}



		//////////////////////////////
		//		Right Weapon		//
		//////////////////////////////

		//The right side weapon of the mech.
		GameObject WeaponRight = Instantiate(mechWeaponRight, new Vector3(transform.position.x, transform.position.y + adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);
		//to get x and z value we take these first as these values are the same no matter.
		if (RocketLauncherRight)
			WeaponRight.transform.localPosition = RocketLauncherPosition;

		else if (GrenadeLauncherRight)
			WeaponRight.transform.localPosition = GrenadeLauncherPosition;

		else if (MortarLauncherRight)
			WeaponRight.transform.localPosition = MortarLauncherPosition;

		//To get the y value we keep the old x and z positions and set the correct y value dependent on the movement module chosen.
		if (mechQuadruped)
		{
			WeaponRight.transform.localPosition = new Vector3 (WeaponRight.transform.localPosition.x, 3f, WeaponRight.transform.localPosition.z);
		}
		else if (mechBiped)
		{
			WeaponRight.transform.localPosition = new Vector3(WeaponRight.transform.localPosition.x, 5.08f, WeaponRight.transform.localPosition.z);
		}
		if (mechTreads)
		{
			WeaponRight.transform.localPosition = new Vector3(WeaponRight.transform.localPosition.x, 3.1f, WeaponRight.transform.localPosition.z);
		}

		//////////////////////////////
		//		 Left Weapon		//
		//////////////////////////////

		//The left side weapon of the mech.
		GameObject WeaponLeft = Instantiate(mechWeaponLeft, new Vector3(transform.position.x, transform.position.y + adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);

		//we need to invert the model as we only have one model of each weapon and we don't have a left sided weapon.
		WeaponLeft.transform.localScale = new Vector3(-1f, 1f, 1f);

		if (RocketLauncherLeft)
			WeaponLeft.transform.localPosition = new Vector3(-RocketLauncherPosition.x, RocketLauncherPosition.y, RocketLauncherPosition.z);

		else if (GrenadeLauncherLeft)
			WeaponLeft.transform.localPosition = new Vector3(-GrenadeLauncherPosition.x, GrenadeLauncherPosition.y, GrenadeLauncherPosition.z);

		else if (MortarLauncherLeft)
			WeaponLeft.transform.localPosition = new Vector3(-MortarLauncherPosition.x, MortarLauncherPosition.y, MortarLauncherPosition.z);

		if (mechQuadruped)
		{
			WeaponLeft.transform.localPosition = new Vector3(WeaponLeft.transform.localPosition.x, 3f, WeaponLeft.transform.localPosition.z);
		}

		if (mechBiped)
		{
			WeaponLeft.transform.localPosition = new Vector3(WeaponLeft.transform.localPosition.x, 5.08f, WeaponLeft.transform.localPosition.z);
		}

		if (mechTreads)
		{
			WeaponLeft.transform.localPosition = new Vector3(WeaponLeft.transform.localPosition.x, 3.1f, WeaponLeft.transform.localPosition.z);
		}

		//////////////////////////////
		//		   Mech Head		//
		//////////////////////////////

		GameObject Head = Instantiate(mechHead, new Vector3(transform.position.x, transform.position.y + adjustment_Movement, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z), gameObject.transform);
		if (mechQuadruped)
		{
			Head.transform.localPosition = new Vector3(0f, 2.417f, 0f);
		}
		if (mechBiped)
		{
			Head.transform.localPosition = new Vector3(0f, 4.323f, 0f);
		}
		if (mechTreads)
		{
			Head.transform.localPosition = new Vector3(0f, 2.39f, 0f);
		}
	}
}
