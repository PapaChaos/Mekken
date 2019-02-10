using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSCeneOnClick : MonoBehaviour {
	public int sceneValue;
    public void LoadByIndex()
    {
		Debug.Log("sceneBuildIndex to load: " + sceneValue);
		SceneManager.LoadScene(sceneValue);
    }
}
