using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechasReadyLoadGame : MonoBehaviour
{

    int buildCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementReady()
    {
        buildCount++;

       

        if (buildCount == 2)
        {
            Debug.Log("LOAD SCENE");
            SceneManager.LoadScene(0); //TODO: lets not hardcode scene indexes
        }


    }
}
