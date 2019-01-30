/*
Created by Dan Wad. 30.01-19
Think this should have everything that is needed. Might need some clean up and better commenting.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

	public List<Vector3> waypointLocation;
	public List<Vector3> waypointRotation;

	public int startingWaypoint = 0;
	public int targetWaypoint = 1;
	public int previousWaypoint;

	public float movementSpeed = 1;
	public bool reverseAtEnd;
	public bool addPosition = true;
	public bool moving = true;
	float waypointDistance;
	float alphaLerp;
	float currentDistance;

	void Awake()
	{
		gameObject.transform.position = waypointLocation[startingWaypoint];
		gameObject.transform.eulerAngles = waypointRotation[startingWaypoint];
		previousWaypoint = startingWaypoint;
		targetWaypoint = startingWaypoint;

	}

    void Update()
    {
		if (moving) //in case we want in game activation and deactivation of object.
			if (waypointLocation.Count > 1) //makes sure that we do this only if we got more than one waypoint.
			{
				float frameMovement = movementSpeed * Time.deltaTime;

				if (targetWaypoint < waypointLocation.Count) //just in case someone deletes the last waypoint in editor during play.
				{
					transform.position = Vector3.MoveTowards(transform.position, waypointLocation[targetWaypoint], frameMovement);


					currentDistance = Vector3.Distance(gameObject.transform.position, waypointLocation[targetWaypoint]);
				}
				if (waypointDistance != 0) //we don't divide by zero.
				{
					alphaLerp = 1 - (currentDistance / waypointDistance);

					transform.eulerAngles = Vector3.Lerp(waypointRotation[previousWaypoint], waypointRotation[targetWaypoint], alphaLerp);
				}

				if (currentDistance < 0.1f || targetWaypoint >= waypointLocation.Count)
				{
					targetWaypoint = NextWaypoint();
					waypointDistance = Vector3.Distance(waypointLocation[previousWaypoint], waypointLocation[targetWaypoint]);
				}
				


			}
        
    }


	int NextWaypoint()
	{
		int waypoint = targetWaypoint;
		previousWaypoint = targetWaypoint;
			switch (addPosition)
			{
				case true:
					waypoint++;//just so I don't need another else statement.
					if (waypoint >= waypointLocation.Count)
					{
						if (reverseAtEnd)
						{
							addPosition = false;
							waypoint--;
							waypoint--;
						}

						else
						{
							waypoint = 0;
						}
					//this last one is just in case someone removes an waypoint in the editor at playtime.
						if (waypoint >= waypointLocation.Count)
						{
							waypoint--;
						}

					}
				return waypoint;

				case false:

				waypoint--;

					if (waypoint < 0)
					{
						addPosition = true;
						waypoint = 1;
					}
				return waypoint;

			}
		return waypoint;
	}
}
