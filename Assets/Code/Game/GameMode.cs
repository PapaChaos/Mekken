using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
	public List<PlayerMovement> l_ActivePlayers;

	void Start()
	{
		l_ActivePlayers.Clear();
		l_ActivePlayers = GetAllActivePlayers();
	}

	// Update is called once per frame
	void Update()
	{

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
