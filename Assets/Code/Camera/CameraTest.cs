using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
	public GameMode gm;
	public List<PlayerMovement> l_players;
	public float playerdistance;
	private void Start()
	{
		l_players = gm.GetAllActivePlayers();
	}

	private void Update()
	{
		List<Vector3> pos = new List<Vector3>();
		pos.Clear();
		foreach (PlayerMovement player in l_players)
		{
			pos.Add(player.gameObject.transform.position);
		}
		Vector3 MinVector = pos.First();
		Vector3 MaxVector = pos.First();
	
		foreach(Vector3 vec in pos)
		{
			//check minimum vector values
			if (MinVector.x > vec.x) 
				MinVector.x = vec.x;
			if (MinVector.y > vec.y)
				MinVector.y = vec.y;
			if (MinVector.z > vec.z)
				MinVector.z = vec.z;

			//check maximum vector values
			if (MaxVector.x < vec.x)
				MaxVector.x = vec.x;
			if (MaxVector.y < vec.y)
				MaxVector.y = vec.y;
			if (MaxVector.z < vec.z)
				MaxVector.z = vec.z;
		}

		Vector3 middlevector = 0.5f*MinVector + 0.5f * MaxVector;
		playerdistance = Vector3.Distance(MinVector, MaxVector);
		transform.LookAt(middlevector);
		middlevector.x = Mathf.Cos(30) + playerdistance;
		middlevector.y = Mathf.Sin(30) + playerdistance + 8;

		gameObject.transform.position = middlevector;
	}
}
