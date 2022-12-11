using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    [SerializeField] int _nextWaypoint;
    [SerializeField] float _waypointOffsetDistance;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] BaseAction<WalkData> _walkAction;

    public virtual void Patrol()
    {
        var isWalking = true;
        //distance is near the next point, need to move to the next point
        var distance = Mathf.Abs(transform.position.x - _waypoints[_nextWaypoint].position.x);
        if (distance < _waypointOffsetDistance)
        {
            isWalking = IsMovingToNextPoint();
        }

        //if (IsNearBound(BoundOffset))//and walking closer to bounds //need implementaion
        //  MoveToNextPoint();
        //check if hit a wall, if so move to next point;

        var direction = 0;
        direction = _waypoints[_nextWaypoint].position.x > transform.position.x ? 1 : -1;

        if (isWalking)
        {
            _walkAction.InitAction(new WalkData(RB,new Vector3(direction,0,0)));
        }
    }

    private bool IsMovingToNextPoint()
    {
        _nextWaypoint++;
        if (_nextWaypoint >= _waypoints.Count)
        {
            _nextWaypoint = 0;
            return false;
        }
        return true;
    }
}
