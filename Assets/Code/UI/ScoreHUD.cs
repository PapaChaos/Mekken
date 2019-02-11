using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
	public GameManager gm;
	public Text player1Score;
	public Text player2Score;
	private void Awake()
	{
		gm = FindObjectOfType<GameManager>();
	}

	// Start is called before the first frame update
	void Start()
    {
		player1Score.text = gm.playerScore[0].ToString();
		player2Score.text = gm.playerScore[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
		player1Score.text = gm.playerScore[0].ToString();
		player2Score.text = gm.playerScore[1].ToString();
	}
}
