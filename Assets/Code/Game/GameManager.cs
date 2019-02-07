using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    public int playerOneRoundsWon = 0;
    public int playerTwoRoundsWon = 0;

    private float timer = -1;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0  )
        {
            if (gameOver && Time.time > timer)
            {
                timer = -1;
                //back to main menu
                SceneManager.LoadScene(1);
            }

        }
    }

    public void setGameOver(bool over)
    {
        gameOver = over;
        //TODO: maybe a short timer prior to launching next scene
        timer = Time.time + 2; //two seconds delay

    }
    public bool getGameOver()
    {
        return gameOver;
    }

}
