using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    [SerializeField] int _nextWaypoint;
    [SerializeField] float _waypointOffsetDistance;
    List<Transform> _waypoints;

    public virtual void Patrol()
    {
        //distance is near the next point, need to move to the next point
        if (Vector3.Distance(transform.position, _waypoints[_nextWaypoint].position) < _waypointOffsetDistance)
            MoveToNextPoint();

        //if (IsNearBound(BoundOffset))//and walking closer to bounds //need implementaion
        //  MoveToNextPoint();
        //check if hit a wall, if so move to next point;

        //Walk(_waypoints[_nextWaypoint].position.x) //walk towards next point
    }

    private void MoveToNextPoint()
    {
            _nextWaypoint = _nextWaypoint < _waypoints.Count ? _nextWaypoint++ : _nextWaypoint = 0;
    }
public virtual void Walk(int xDirection, float speed)
{
    var velocity = new Vector3(xDirection * _speed, RB.velocity.y, 0);
    RB.velocity = velocity;
}
}
