using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    [SerializeField] int _nextWaypoint;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] BaseAction<WalkData> _walkAction;
    [SerializeField] CheckXDistanceAction _boundsXDistanceAction;
    [SerializeField] CheckXDistanceAction _waypointXDistanceAction;
    [SerializeField] GroundSensorInfo _groundSensorInfo;
    WalkData _walkData;
    private void OnEnable()
    {
        _groundSensorInfo.SubscribeToEvents(SensorHandler);
    }
    private void OnDisable()
    {
        _groundSensorInfo.UnsubscribeToEvents(SensorHandler);
    }
    private void Start()
    {
        _walkData = new WalkData(RB, Vector3.zero);
    }

    public override void CheckValidation()
    {
        base.CheckValidation();
        //if (!_groundCheckSensor)
        //    throw new System.Exception("GroundEnemy has no ground check sensor");
    }
    public virtual void Patrol()
    {
        if (!_groundSensorInfo.IsGrounded)
            return;

        bool moveToNextPoint = false;//check if need to move to next point
        if (_waypointXDistanceAction.InitAction(new DistanceData(transform.position, _waypoints[_nextWaypoint].position, _waypointXDistanceAction.Distance)))
        {
            moveToNextPoint = true;
        }

        if (_boundsXDistanceAction.InitAction(new DistanceData(transform.position, Bound.max, _boundsXDistanceAction.Distance))
            || _boundsXDistanceAction.InitAction(new DistanceData(transform.position, Bound.min, _boundsXDistanceAction.Distance)))
        {
            moveToNextPoint = true;
        }

        if (_groundSensorInfo.SensorInfoType == SensorInfoType.PartialHit)
        {
            moveToNextPoint = true;
        }

        //checked Ledge

        //check if hit a wall, if so move to next point;
        //check front with raycast
        //if raycast hit object that is tagged ground move to next point = true
        //
        if (moveToNextPoint)
        {
            IsMovingToNextPoint();
        }
        var direction = 0;
        direction = _waypoints[_nextWaypoint].position.x > transform.position.x ? 1 : -1;

        _walkData.Direction = new Vector3(direction, 0, 0);
        _walkAction.InitAction(_walkData);
    }

    private bool IsMovingToNextPoint()
    {
        _nextWaypoint++;
        if (_nextWaypoint >= _waypoints.Count)
        {
            _nextWaypoint = 0;
        }
        return true;
    }
}
