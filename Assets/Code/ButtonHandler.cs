using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//dirty
public class ButtonHandler : MonoBehaviour
{
	private GameManager gm;
	public void LoadScene(int scene)
    {
		if (!gm)
			gm = FindObjectOfType<GameManager>();
		if (gm)
		Destroy(gm.gameObject);

		foreach (PlayerMech mech in FindObjectsOfType<PlayerMech>())
			Destroy(mech.gameObject);



        SceneManager.LoadScene(scene);
        Debug.Log("Loading scene " + scene);
    }

}
