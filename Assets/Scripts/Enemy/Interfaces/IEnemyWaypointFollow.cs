using UnityEngine;

public interface IEnemyWaypointFollow
{
    EnemyStats enemyStats { get; }

    WaypointManager waypointManager { get; set; }

    float speed { get; set; }

    void GetNextWaypoint();

    void MoveEnemy();
}
