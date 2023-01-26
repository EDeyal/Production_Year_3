using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    [SerializeField] int _nextWaypoint;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] BaseAction<MoveData> _moveAction;
    [SerializeField] CheckXDistanceAction _boundsXDistanceAction;
    [SerializeField] CheckXDistanceAction _waypointXDistanceAction;
    [SerializeField] GroundSensorInfo _groundSensorInfo;
    [SerializeField] WallSensorInfo _rightWallSensorInfo;
    [SerializeField] WallSensorInfo _leftWallSensorInfo;
    [SerializeField] LedgeSensorInfo _ledgeSensorInfo;
    MoveData _moveData;

    public CheckXDistanceAction BoundsXDistanceAction => _boundsXDistanceAction;
    public override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        _groundSensorInfo.SubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.SubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.SubscribeToEvents(SensorHandler);
        _ledgeSensorInfo.SubscribeToEvents(SensorHandler);
    }
    private void OnDisable()
    {
        _groundSensorInfo.UnsubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _ledgeSensorInfo.UnsubscribeToEvents(SensorHandler);
    }
    private void Start()
    {
        _moveData = new MoveData(RB, Vector3.zero,EnemyStatSheet.Speed);
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
        if (_waypointXDistanceAction.InitAction(new DistanceData(transform.position, _waypoints[_nextWaypoint].position)))
        {
            moveToNextPoint = true;
        }

        if (_boundsXDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.max))
            || _boundsXDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.min)))
        {
            moveToNextPoint = true;
        }

        //change direction if near a ledge
        if (_ledgeSensorInfo.SensorInfoType == SensorInfoType.PartialHit)
        {
            if (_groundSensorInfo.SensorInfoType == SensorInfoType.PartialHit)
            {
                moveToNextPoint = true;
            }
        }
        //wall Check
        if (_rightWallSensorInfo.IsNearWall||_leftWallSensorInfo.IsNearWall)
        {
            moveToNextPoint = true;
        }

        //if needed to turn for some reason, turn to next waypoint
        if (moveToNextPoint)
        {
            IsMovingToNextPoint();
        }
        var direction = ZERO;
        direction = _waypoints[_nextWaypoint].position.x > transform.position.x ? ONE :MINUS_ONE;

        _moveData.UpdateData(new Vector3(direction, ZERO, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);
    }
    public virtual void StopMovement()
    {
        _moveData.UpdateData(ZERO);
        _moveAction.InitAction(_moveData);
    }
    public virtual void Chase()
    {
        //find player and determin his direction
        var direction = GeneralFunctions.GetXDirectionToTarget(transform.position,GameManager.Instance.PlayerManager.transform.position);
        _moveData.UpdateData(new Vector3(direction, ZERO, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);
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
