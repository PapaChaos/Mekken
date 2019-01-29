using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    
    public void LoadByIndex()
    {

        Debug.Log("HELLO LOAD");
        SceneManager.LoadScene(1);
    }

}
