using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class InitCountDown : MonoBehaviour
{
    public int timeLeft = 60; //Seconds Overall
    public Text countdown; //UI Text Object
    public Text getReady;
    public Text fight;
    public Canvas scoreDisplay;
    bool canvasTurnedOn = false;
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
        scoreDisplay.GetComponent<Canvas>().enabled = false;
    }
    void Update()
    {
        if (timeLeft >= 0)
        {
            countdown.text = ("" + timeLeft); //Showing the Score on the Canvas
            getReady.text = "GET READY!";
            fight.text = "";
           
        }
        else
        {
            if (timeLeft >= -2)
            {
                countdown.text = ("");
                getReady.text = ("");
                fight.text = ("FIGHT!");
            }
            else
            {
                countdown.text = ("");
                getReady.text = ("");
                fight.text = ("");
                if (!canvasTurnedOn)
                   {
                   
                    scoreDisplay.GetComponent<Canvas>().enabled = true;
                    canvasTurnedOn = true;
                   }
            }
        }
    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}