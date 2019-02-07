/*	Created by Dan Wad.
 * Old game manager made by Dan Wad. Removed for Assets/Code/Game/GameManager.cs
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
	public List<PlayerMovement> l_ActivePlayers;

	void Start()
	{
		l_ActivePlayers.Clear(); //I like to always clear lists in case of old data. 
		l_ActivePlayers = GetAllActivePlayers();
	}

	public List<PlayerMovement> GetAllActivePlayers()   //A simple public function to call for player check in case we want more than 2 players later.
	{
		List<PlayerMovement> ActivePlayersLeft = new List<PlayerMovement>();

		foreach (PlayerMovement player in FindObjectsOfType(typeof(PlayerMovement)) as PlayerMovement[])
		{
			//if((player.activeInHierarchy))
			ActivePlayersLeft.Add(player);
		}

		return ActivePlayersLeft;
	}
}
