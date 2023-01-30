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

    [SerializeField] RandomMovementSO _randomMovementSO;

    Vector2 _randomPoint;
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
        base.OnDeath();
        RB.useGravity = true;
    }
    public override void Awake()
    {
        base.Awake();
        _idleCooldown = new ActionCooldown();
        _randomPoint = Vector3.zero;
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
    protected virtual bool CheckBounds()
    {
        if (_boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.max))
|| _boundsXYDistanceAction.InitAction(new DistanceData(transform.position, BoundHandler.Bound.min)))
        {
            return true;
        }
        return false;
    }
    protected virtual bool CheckWaypoint(Vector3 myPosition, Vector3 targetPosition, bool checkForCooldown, out bool returnBack)
    {
        if (_waypointXYDistanceAction.InitAction(new DistanceData(myPosition, targetPosition)))
        {
            if (checkForCooldown)
            {
                if (CheckForCooldown(_idleMovementAction, _idleCooldown))
                {
                    //Debug.Log("Waiting for movement cooldown");
                    returnBack = false;
                }
                else
                {
                    returnBack = true;
                    return true;
                }
            }
            returnBack = false;
            return true;
        }
        returnBack = false;
        return false;
    }
    protected virtual bool CheckWalls()
    {
        if (_groundSensorInfo.IsNearWall || _ceilingSensorInfo.IsNearWall ||
    _rightWallSensorInfo.IsNearWall || _leftWallSensorInfo.IsNearWall)
        {
            return true;
        }
        return false;
    }

    public virtual void Patrol()
    {
        bool moveToNextPoint = false;
        //check if need to move to next point
        //check if reached the next waypoint
        if (CheckWaypoint(transform.position, _waypoints[_nextWaypoint].position, true, out bool returnBack))
        {
            if (returnBack)
            {
                return;
            }
            moveToNextPoint = true;
        }
        if (CheckBounds())
        {
            moveToNextPoint = true;
        }
        if (CheckWalls())
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
        var direction = GetNormilizedDirectionToTarget(position, target);
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);
    }
    public virtual void StopMovement()
    {
        _moveData.UpdateData(ZERO);
        _moveAction.InitAction(_moveData);
    }
    protected Vector3 GetNormilizedDirectionToTarget(Vector3 myPosition, Vector3 targetPosition)
    {
        var direction = targetPosition - myPosition;
        direction.Normalize();
        return direction;
    }
    public virtual void MoveTo(Vector3 target, float speedMultiplyer = 1)
    {
        Vector3 direction = GetNormilizedDirectionToTarget(transform.position, target);
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed * speedMultiplyer);
        LookAtTarget(target);
        _moveAction.InitAction(_moveData);
    }
    protected void LookAtTarget(Vector3 targetPos)
    {
        //Rotation Assignment
        Vector3 rotation = EnemyVisualHolder.transform.eulerAngles;
        EnemyVisualHolder.transform.LookAt(targetPos);
        EnemyVisualHolder.transform.eulerAngles = new Vector3(EnemyVisualHolder.transform.eulerAngles.x, rotation.y, rotation.z);
    }
    protected void ResetLookAt()
    {
        Vector3 rotation = EnemyVisualHolder.transform.eulerAngles;
        EnemyVisualHolder.transform.eulerAngles = new Vector3(ZERO, rotation.y, rotation.z);
    }
    public virtual void RandomMovement()
    {
        //need to make checks and get random position
        bool getNextPoint = false;
        if (_randomPoint == Vector2.zero)
        {
            GetNewRandomPoint();
        }

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 target = new Vector2(_randomPoint.x, _randomPoint.y);
        var direction = target - position;
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);

        if (CheckWaypoint(transform.position, _randomPoint, false, out bool returnBack))
        {
            getNextPoint = true;
        }
        if (CheckBounds())
        {
            getNextPoint = true;
        }
        if (CheckWalls())
        {
            getNextPoint = true;
        }
        //if one check showed that we should change the random point, create a new point.
        if (getNextPoint)
        {
            GetNewRandomPoint();
        }
    }
    protected int CheckNewPointValidDirection(bool isTouchingPositiveWall, bool isTouchingNegativeWall, int currentNum)
    {
        if (isTouchingPositiveWall && isTouchingNegativeWall)
        {
            return 0;
        }
        else if (isTouchingPositiveWall)
        {
            if (currentNum > 0)
                currentNum *= -1;
        }
        else if (isTouchingNegativeWall)
        {
            if (currentNum < 0)
                currentNum *= -1;
        }
        return currentNum;
    }
    protected virtual void GetNewRandomPoint()
    {
        //need to add logic for sensors and limitations based on them
        int x = Random.Range(_randomMovementSO.RandomDirection.x, _randomMovementSO.RandomDirection.y);
        x = CheckNewPointValidDirection(_rightWallSensorInfo.IsNearWall, _leftWallSensorInfo.IsNearWall, x);
        var y = Random.Range(_randomMovementSO.RandomDirection.x, _randomMovementSO.RandomDirection.y);
        y = CheckNewPointValidDirection(_ceilingSensorInfo.IsNearWall, _groundSensorInfo.IsNearWall, y);
        var direction = new Vector2(x, y);
        direction.Normalize();
        var length = Random.Range(_randomMovementSO.RandomLength.x, _randomMovementSO.RandomLength.y);
        direction = direction * length;
        _randomPoint = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, ZERO);
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

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}