using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCredits : MonoBehaviour
{

    // Timer controls
    private float startTime = 0f;
    private float timer = 0f;
    public float holdTime = 0f; // how long you need to hold to trigger the effect
    public const float TIME_LIMIT = 100f; // Set time for how long the player should be in the credits scene
    // Use if you only want to call the method once after holding for the required time
    private bool held = false;

    public string key = ""; // Whichever key you're using to control the effects. Just hardcode it in if you want
    public string Scene = "MainMenu"; // Whichever scene you want to go to. Can be Hardcoded

    void Update()
    {
        // Starts the timer from when the key is pressed
        if (Input.GetKeyDown(key))
        {
            startTime = Time.time;
            timer = startTime;
        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetKey(key) && held == false)
        {
            timer += Time.deltaTime;

            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timer > (startTime + holdTime))
            {
                held = true;
                ButtonHeld();
                SceneManager.LoadScene(Scene);
            }
        }

        // For single effects. Remove if not needed
        if (Input.GetKeyUp(key))
        {
            held = false;
        }

        // Starts timer
        this.timer += Time.deltaTime;
        
        // Will change scene when the timer goes to what the TIME_LIMIT is set at
        if (this.timer >= TIME_LIMIT)
        {
            SceneManager.LoadScene(Scene);
        }
    }

    // Method called after held for required time
    void ButtonHeld()
    {
        Debug.Log("held for " + holdTime + " seconds");
    }
}

