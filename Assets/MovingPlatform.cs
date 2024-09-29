using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance = 0.05f;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;

    // Set intial waypoint to first in array
    void Start()
    {
        _targetWaypoint = _waypoints[0];
    }

    // Platform uses MoveTowards fucntion to move towards currently targetted waypoint.
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetWaypoint.position) < _checkDistance)
        {
            _targetWaypoint = GetNextWaypoint();
        }
    }

    // This function is called when platform has reached a waypoint. It iterates to the next waypoint in the array. If the platform has hit the last waypoint, it goes back to the first waypoint in the array.
    private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= _waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }

        return _waypoints[_currentWaypointIndex];
    }

    // While the player is colliding with the platform, it is a transform child of the platform. This keeps the player on the platform while it moves.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    // When the player leaves the platform, it becomes independent of the platform.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
}
