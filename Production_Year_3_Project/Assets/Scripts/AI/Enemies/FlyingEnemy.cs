using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEnemy : BaseEnemy
{
    int _nextWaypoint;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] BaseAction<MoveData> _moveAction;
    MoveData _moveData;
    [SerializeField] CheckXYDistanceAction _boundsXYDistanceAction;
    [SerializeField] CheckXYDistanceAction _waypointXYDistanceAction;
    [SerializeField] BaseAction<ActionCooldownData> _idleMovementAction;
    ActionCooldown _idleCooldown;

    [SerializeField] GroundSensorInfo _groundSensorInfo;
    [SerializeField] WallSensorInfo _rightWallSensorInfo;
    [SerializeField] WallSensorInfo _leftWallSensorInfo;
    [SerializeField] CeilingSensorInfo _ceilingSensorInfo;

    private void OnEnable()
    {
        _groundSensorInfo.SubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.SubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.SubscribeToEvents(SensorHandler);
        _ceilingSensorInfo.SubscribeToEvents(SensorHandler);
    }
    private void OnDisable()
    {
        _groundSensorInfo.UnsubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _ceilingSensorInfo.UnsubscribeToEvents(SensorHandler);
    }
    public override void OnDeath()
    {
        throw new System.NotImplementedException();
    }
    public override void Awake()
    {
        base.Awake();
        _idleCooldown = new ActionCooldown();
    }
    public bool IdleWaitAction()
    {
        //wait for X seconds than move to the next point
        return WaitAction(_idleMovementAction, ref _idleCooldown);
    }
    public void Start()
    {
        _moveData = new MoveData(RB, Vector3.zero, EnemyStatSheet.Speed);
    }
    public virtual void Patrol()
    {
        bool moveToNextPoint = false;//check if need to move to next point
        if (_waypointXYDistanceAction.InitAction(new DistanceData(transform.position, _waypoints[_nextWaypoint].position)))
        {
            moveToNextPoint = true;
        }
        if (_boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.max))
    || _boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.min)))
        {
            moveToNextPoint = true;
        }
    }
}
