/*
Created by Dan Wad. 30.01-19
ToDo: add a "add waypoint at number. Then move all waypoints above that number up by one. So if we have 5 waypoints and we want to add a waypoint at number 3. Then 3 becomes 4 and 4 becomes 5 (since we start at 0).


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(MovingObject))]
public class AddWaypoint : Editor
{
	//private variables 
	bool showWaypoints = true;
	bool objectDebug = false;
	int moveToWaypoint;


	public override void OnInspectorGUI()
	{
		MovingObject mo = (MovingObject)target;

		GUILayout.Label("Current Target Waypoint: " + mo.targetWaypoint);
		if (objectDebug)
			GUILayout.Label("Previous Waypoint: " + mo.previousWaypoint);
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		mo.movementSpeed = EditorGUILayout.FloatField("Movement Speed: ", mo.movementSpeed);
		mo.startingWaypoint = EditorGUILayout.IntField("Starting Waypoint: ", mo.startingWaypoint);


		//Just to make sure that the editer doesn't enter a bad value.
		if (mo.startingWaypoint < 0 || mo.startingWaypoint >= mo.waypointLocation.Count && mo.waypointLocation.Count > 0)
		{
			//Just to make sure that the editer doesn't get confused by why the value is set to 0.
			Debug.Log("There is no " + mo.startingWaypoint + " waypoint. Setting to 0.");

			mo.startingWaypoint = 0;
		}

		mo.reverseAtEnd = EditorGUILayout.Toggle("Reverse at the end", mo.reverseAtEnd);

		if (objectDebug)
			mo.addPosition = EditorGUILayout.Toggle("adding to waypoint", mo.addPosition);

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Move to Waypoint: "))
		{
			mo.transform.position = mo.waypointLocation[moveToWaypoint];
			mo.transform.eulerAngles = mo.waypointRotation[moveToWaypoint];
		}
		moveToWaypoint = EditorGUILayout.IntField(moveToWaypoint);

		//Just to make sure that the editer doesn't enter a bad value.
		if (moveToWaypoint >= mo.waypointLocation.Count || moveToWaypoint < 0)
		{
			//Just to make sure that the editer doesn't get confused by why the value is set to 0.
			Debug.Log("There is no " + moveToWaypoint +" waypoint. Setting to 0.");
			moveToWaypoint = 0;
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

		if (GUILayout.Button("Add Current Location and Rotation"))
		{
			mo.waypointLocation.Add(mo.transform.position);
			mo.waypointRotation.Add(mo.transform.rotation.eulerAngles);
		}


		//Adding a fold out at the end for all the waypoints. This way EditorGUI doesn't look so massive.
		showWaypoints = EditorGUILayout.Foldout(showWaypoints, "Waypoints: " + mo.waypointLocation.Count);
		if (showWaypoints)
			if (Selection.activeTransform)
			{
				for (int listcount = 0; listcount < mo.waypointLocation.Count; listcount++)
				{
					EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
					GUILayout.BeginHorizontal();
					GUILayout.Label("     Waypoint: " + listcount);
					if(GUILayout.Button("Remove")){
						mo.waypointLocation.RemoveAt(listcount);
						mo.waypointRotation.RemoveAt(listcount);
					}
					GUILayout.EndHorizontal();
					mo.waypointLocation[listcount] = EditorGUILayout.Vector3Field("Waypoint Location:", mo.waypointLocation[listcount]);
					mo.waypointRotation[listcount] = EditorGUILayout.Vector3Field("Waypoint Rotation:", mo.waypointRotation[listcount]);
					
					GUILayout.Space(5);
				}
				EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


			}
		if (!Selection.activeTransform)
		{
			showWaypoints = false;
		}
	}

}
