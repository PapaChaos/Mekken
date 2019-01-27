//
//Created by Dan Wad. To handle Mech Construction from UI.
//TODO: Add a check if both players are ready before constructing mechs. Might want to do that in the GameManager.
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechConstructor : MonoBehaviour
{
	//the chosen mech parts.
	public GameObject Player_Frame, Player_Movement, Player_RightWeapon, Player_LeftWeapon, Player_Head;

	//each gameobject for each button press. Was thinking about using lists instead but it might just make the code look more complicated then necessary.
	public GameObject MechFrame01, MechTreads, MechBiped, MechQuadruped, MechRocketLauncherRight, MechGrenadeLauncherRight;
	public GameObject MechMortarLauncherRight, MechRocketLauncherLeft, MechGrenadeLauncherLeft, MechMortarLauncherLeft, MechHead01, MechHead02, MechHead03;

	public GameObject Player;
	public GameObject PlayerSpawn;
	public bool PlayerReady;

	//int Player is set up in case we want to add more than 2 players later to each fight.
	public void Player_Frame01_Button(int Player)
	{
		Player_Frame = MechFrame01;
	}
	public void Player_Treads_Button(int Player)
	{
		Player_Movement = MechTreads;
	}
	public void Player_Biped_Button(int Player)
	{
		Player_Movement = MechBiped;
	}
	public void Player_Quadruped_Button(int Player)
	{
		Player_Movement = MechQuadruped;
	}
	public void Player_RocketLauncherRight_Button(int Player)
	{
		Player_RightWeapon = MechRocketLauncherRight;
	}
	public void Player_GrenadeLauncherRight_Button(int Player)
	{
		Player_RightWeapon = MechGrenadeLauncherRight;
	}
	public void Player_MortarLauncherRight_Button(int Player)
	{
		Player_RightWeapon = MechMortarLauncherRight;
	}
	public void Player_RocketLauncherLeft_Button(int Player)
	{
		Player_LeftWeapon = MechRocketLauncherLeft;
	}
	public void Player_GrenadeLauncherLeft_Button(int Player)
	{
		Player_LeftWeapon = MechGrenadeLauncherLeft;
	}
	public void Player_MortarLauncherLeft_Button(int Player)
	{
		Player_LeftWeapon = MechMortarLauncherLeft;
	}
	public void Player_Head01_Button(int Player)
	{
		Player_Head = MechHead01;
	}
	public void Player_Head02_Button(int Player)
	{
		Player_Head = MechHead02;
	}
	public void Player_Head03_Button(int Player)
	{
		Player_Head = MechHead03;
	}
	public void Player_Start_Button(int Player)
	{
		if (Player_Frame && Player_Movement && Player_RightWeapon && Player_LeftWeapon && Player_Head)
		{
			BothPlayersReady();
		}
	}

	//This function is to generate the mechs and start the match.
	void BothPlayersReady()
	{

		print("Match is starting!");

		//Player_Frame, Player_Movement, Player_RightWeapon, Player_LeftWeapon, Player_Head;

		GameObject player = Instantiate(Player, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
		PlayerMech pMech = player.GetComponent<PlayerMech>();
		pMech.mechFrame = Player_Frame;
		pMech.mechHead = Player_Head;
		pMech.mechMovement = Player_Movement;
		pMech.mechWeaponRight = Player_RightWeapon;
		pMech.mechWeaponLeft = Player_LeftWeapon;
		pMech.ConstructMecha();

		Destroy(this.gameObject);
	}
}
