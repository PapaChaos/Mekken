//
//Created by Dan Wad. To handle Mech Construction from UI.
//TODO: Add a check if both players are ready before constructing mechs. Might want to do that in the GameManager.
//31.Jan added in enums instead for checks.
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechConstructor : MonoBehaviour
{
    //<JPK>
    public MechasReadyLoadGame loader;      //quick and easy way to load arena
    public int playerNumber = 0;            //player number assigned

	public GameManager gameManager; //setting the Game Manager here.

    //the chosen mech parts.
    public enum Module_Frame { None, Frame01 };
	public enum Module_Movement { None, Treads, Biped, Quadruped };
	public enum Module_RightWeapon { None, RocketLauncher, GrenadeLauncher, MortarLauncher };
	public enum Module_LeftWeapon { None, RocketLauncher, GrenadeLauncher, MortarLauncher };
	public enum Module_HeadModule { None, Head01, Head02, Head03 };

	public Module_Frame chosenFrame = Module_Frame.None;
	public Module_Movement chosenMovement = Module_Movement.None;
	public Module_RightWeapon chosenRightWeapon = Module_RightWeapon.None;
	public Module_LeftWeapon chosenLeftWeapon = Module_LeftWeapon.None;
	public Module_HeadModule chosenHead = Module_HeadModule.None;


	//each gameobject for each button press. Was thinking about using lists instead but it might just make the code look more complicated then necessary.
	public GameObject MechFrame01, MechTreads, MechBiped, MechQuadruped, MechRocketLauncherRight, MechGrenadeLauncherRight;
	public GameObject MechMortarLauncherRight, MechRocketLauncherLeft, MechGrenadeLauncherLeft, MechMortarLauncherLeft, MechHead01, MechHead02, MechHead03;

	public GameObject Player;
	public GameObject PlayerSpawn;
	public bool PlayerReady;

    

	//int Player is set up in case we want to add more than 2 players later to each fight.
	public void Player_Frame01_Button(int Player)
	{
		chosenFrame = Module_Frame.Frame01;
	}

	public void Player_Treads_Button(int Player)
	{
		chosenMovement = Module_Movement.Treads;
	}

	public void Player_Biped_Button(int Player)
	{
		chosenMovement = Module_Movement.Biped;
	}

	public void Player_Quadruped_Button(int Player)
	{
		chosenMovement = Module_Movement.Quadruped;
	}

	public void Player_RocketLauncherRight_Button(int Player)
	{
		chosenRightWeapon = Module_RightWeapon.RocketLauncher;
	}

	public void Player_GrenadeLauncherRight_Button(int Player)
	{
		chosenRightWeapon = Module_RightWeapon.GrenadeLauncher;
	}

	public void Player_MortarLauncherRight_Button(int Player)
	{
		chosenRightWeapon = Module_RightWeapon.MortarLauncher;
	}

	public void Player_RocketLauncherLeft_Button(int Player)
	{
		chosenLeftWeapon = Module_LeftWeapon.RocketLauncher;
	}

	public void Player_GrenadeLauncherLeft_Button(int Player)
	{
		chosenLeftWeapon = Module_LeftWeapon.GrenadeLauncher;
	}

	public void Player_MortarLauncherLeft_Button(int Player)
	{
		chosenLeftWeapon = Module_LeftWeapon.MortarLauncher;
	}

	public void Player_Head01_Button(int Player)
	{
		chosenHead = Module_HeadModule.Head01;
	}

	public void Player_Head02_Button(int Player)
	{
		chosenHead = Module_HeadModule.Head02;
	}

	public void Player_Head03_Button(int Player)
	{
		chosenHead = Module_HeadModule.Head03;
	}

	//this is the start button to check if both players are ready.
	public void Player_Start_Button(int Player)
	{
		if(chosenFrame != Module_Frame.None && chosenMovement != Module_Movement.None && chosenRightWeapon != Module_RightWeapon.None &&
			chosenLeftWeapon != Module_LeftWeapon.None && chosenHead != Module_HeadModule.None)
		BothPlayersReady();
	}

    //This function is to generate the mechs and start the match.
    
	void BothPlayersReady()
	{
		//Player_Frame, Player_Movement, Player_RightWeapon, Player_LeftWeapon, Player_Head;

		GameObject player = Instantiate(Player, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
		PlayerMech pMech = player.GetComponent<PlayerMech>();

		//mech treads is the only movement module that does not include the frame.
		if (chosenMovement == Module_Movement.Treads)
			switch (chosenFrame)
			{
				case Module_Frame.Frame01:
					pMech.mechFrame = MechFrame01;
					break;
			}

		switch (chosenMovement)
		{
			case Module_Movement.Treads:
				pMech.mechMovement = MechTreads;
				pMech.mechTreads = true;
				break;
			case Module_Movement.Biped:
				pMech.mechMovement = MechBiped;
				pMech.mechBiped = true;
				break;
			case Module_Movement.Quadruped:
				pMech.mechMovement = MechQuadruped;
				pMech.mechQuadruped = true;
				break;
		}

		switch (chosenRightWeapon)
		{
			case Module_RightWeapon.RocketLauncher:
				pMech.mechWeaponRight = MechRocketLauncherRight;
				pMech.RocketLauncherRight = true;
				break;
			case Module_RightWeapon.GrenadeLauncher:
				pMech.mechWeaponRight = MechGrenadeLauncherRight;
				pMech.GrenadeLauncherRight = true;
				break;
			case Module_RightWeapon.MortarLauncher:
				pMech.mechWeaponRight = MechMortarLauncherRight;
				pMech.MortarLauncherRight = true;
				break;
		}

		switch(chosenLeftWeapon)
		{
			case Module_LeftWeapon.RocketLauncher:
				pMech.mechWeaponLeft = MechRocketLauncherLeft;
				pMech.RocketLauncherLeft = true;
				break;
			case Module_LeftWeapon.GrenadeLauncher:
				pMech.mechWeaponLeft = MechGrenadeLauncherLeft;
				pMech.GrenadeLauncherLeft = true;
				break;
			case Module_LeftWeapon.MortarLauncher:
				pMech.mechWeaponLeft = MechMortarLauncherLeft;
				pMech.MortarLauncherLeft = true;
				break;

		}

		switch (chosenHead)
		{
			case Module_HeadModule.Head01:
				pMech.mechHead = MechHead01;
				break;
			case Module_HeadModule.Head02:
				pMech.mechHead = MechHead02;
				break;
			case Module_HeadModule.Head03:
				pMech.mechHead = MechHead03;
				break;
		}


      
        pMech.ConstructMecha(playerNumber);
        DontDestroyOnLoad(pMech);

        Destroy(this.gameObject);

		gameManager.PlayersReady();

		//loader.incrementReady();

	}
}
