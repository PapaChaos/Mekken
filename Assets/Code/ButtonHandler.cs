﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//dirty
public class ButtonHandler : MonoBehaviour
{

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Debug.Log("Loading scene " + scene);
    }

}
