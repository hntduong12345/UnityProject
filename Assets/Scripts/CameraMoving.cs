using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMoving : MonoBehaviour
{
	public Tilemap groundTileMap;
	public float cameraSpeed = 5f;
	public Transform[] waypoints; // Define waypoints for the camera to follow
	private int currentWaypointIndex = 0;
	// Start is called before the first frame update
	void Start()
	{
		if (groundTileMap != null && waypoints.Length > 0)
		{
			// Set the initial position of the camera to the first waypoint
			transform.position = waypoints[0].position;
		}
		else
		{
			Debug.LogError("Ground Tilemap or waypoints are not assigned");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (groundTileMap == null || waypoints.Length == 0)
			return;

		// Move the camera towards the current waypoint
		Vector3 targetPosition = waypoints[currentWaypointIndex].position;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

		// Check if the camera has reached the current waypoint
		if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
		{
			// Move to the next waypoint
			currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
		}
	}
}
