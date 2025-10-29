using UnityEngine;

public interface IWaypointFollow
{
    float speed { get; set; }

    Transform target {  get; set; }

    int waypointIndex { get; set; }

    void GetNextWaypoint();

    void MoveEnemy();
}
