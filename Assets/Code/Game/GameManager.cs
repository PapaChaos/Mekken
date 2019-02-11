using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false, roundReady = false;
	public int playersReady = 0, sceneIndex = 1; //Players Ready is for constructionUI. sceneIndex is for the initial scene where this spawns (1 is constructionUI)
	public float nextSceneTimer = 2; //time it takes before next scene appears.
	private float gameOverCurrentTimer; //Just the amount of time that has passed.

	public bool debugIgnoreCountdown = false;

	public int alive = 0;
	public int playerNumber = 0;

	//Using a list in case we want to have more than 2 players later. People been talking about having up to 4 players in game. Still hard coded. But, makes it easier if functions already uses a list.
	public List<int> playerScore = new List<int>()
	{
		0,
		0
	}; 

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}


	//I couldn't use OnLevelWasLoaded anymore. But I found alternative solution here https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += levelLoaded;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= levelLoaded;
	}

	void levelLoaded(Scene scene, LoadSceneMode mode)
	{
		setGameOver(false);
		gameOverCurrentTimer = 0;
		roundReady = false;
	}

	public int giveScore(int player)
	{
		int totalScore = playerScore[player]++;
		return totalScore;
	}

	public void resetScore()
	{
		for (int p = 0; p < playerScore.Count; p++)
			playerScore[p] = 0;
	}

	public void nextScene()
	{
		sceneIndex++;
		
		SceneManager.LoadScene(sceneIndex);
	}

	void Update()
	{
		if (getGameOver())
		{
			gameOverCurrentTimer += Time.deltaTime;

			if (gameOverCurrentTimer > nextSceneTimer)
			{
				nextScene();
			}
		}
    }

    public void setGameOver(bool over)
    {
		gameOver = over;
	}

    public bool getGameOver()
    {
        return gameOver;
    }

	public void setRoundReady(bool ready)
	{
		roundReady = ready;
	}

	public bool getRoundReady()
	{
		return roundReady;
	}

	//this is for ConstructionUI check.
	public void PlayersReady()
	{
		playersReady++;
		if (playersReady == 2)
		{
			playersReady = 0; // just to make sure that it sets it self to 0 even though the player quits early.
			nextScene();
		}
	}

	public void CheckRemainingPlayers()
	{
		alive = 0;
		playerNumber = 0;
		foreach (playerProperties player in FindObjectsOfType<playerProperties>())
		{
			if (player.lost == false)
			{
				alive++;
				Debug.Log("Players: " + alive);
				Debug.Log("PlayerNumber alive: " + playerNumber);
				Debug.Log("Player component found: " + player.gameObject.GetComponent<playerProperties>().name);
				playerNumber = player.GetComponent<playerProperties>().playerNumber; 
			}
		}
		if (alive < 2)
		{
			Debug.Log("Players alive: " + alive);
			if (alive == 1)     //just so no score is given if both players died at the same time.
				giveScore(playerNumber-1); //reducing by 1 as playerNumber starts at 1 and list starts at 0
			setGameOver(true);
		}
	}
}
