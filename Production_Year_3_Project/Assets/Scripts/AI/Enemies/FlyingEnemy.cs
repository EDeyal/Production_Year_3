using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEnemy : BaseEnemy
{
    [SerializeField] int _nextWaypoint;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] BaseAction<MoveData> _moveAction;
    MoveData _moveData;
    [SerializeField] CheckXYDistanceAction _boundsXYDistanceAction;
    [SerializeField] CheckXYDistanceAction _waypointXYDistanceAction;
    [SerializeField] BaseAction<ActionCooldownData> _idleMovementAction;
    ActionCooldown _idleCooldown;

    [SerializeField] WallSensorInfo _groundSensorInfo;
    [SerializeField] WallSensorInfo _rightWallSensorInfo;
    [SerializeField] WallSensorInfo _leftWallSensorInfo;
    [SerializeField] WallSensorInfo _ceilingSensorInfo;

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
        //check if reached the next waypoint
        if (_waypointXYDistanceAction.InitAction(new DistanceData(transform.position, _waypoints[_nextWaypoint].position)))
        {
            if(!CheckForCooldown(_idleMovementAction, _idleCooldown))
            {
                Debug.Log("Waiting for movement cooldown");
                return;
            }
            moveToNextPoint = true;
        }
        if (_boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.max))
    || _boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.min)))
        {
            moveToNextPoint = true;
        }

        //check walls around you
        if (_groundSensorInfo.IsNearWall || _ceilingSensorInfo.IsNearWall||
            _rightWallSensorInfo.IsNearWall||_leftWallSensorInfo.IsNearWall)
        {
            moveToNextPoint = true;
        }
        //if needed to turn for some reason, turn to next waypoint
        if (moveToNextPoint)
        {
            IsMovingToNextPoint();
        }
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 target = new Vector2(_waypoints[_nextWaypoint].position.x, _waypoints[_nextWaypoint].position.y);
        var direction =  target - position;
        direction.Normalize();
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed);
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
        var playerPos = GameManager.Instance.PlayerManager.transform.position;

        Vector2 direction = new Vector2(GeneralFunctions.GetXDirectionToTarget(transform.position, playerPos),
            GeneralFunctions.GetYDirectionToTarget(transform.position, playerPos));

        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed);
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
