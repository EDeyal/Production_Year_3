using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEnemy : BaseEnemy
{
    [TabGroup("Locomotion")]
    [SerializeField] int _startingPointIndex = 0;
    [TabGroup("Locomotion")]
    [ReadOnly][SerializeField] int _nextWaypoint;
    [TabGroup("Locomotion")]
    [SerializeField] List<Transform> _waypoints;
    [TabGroup("Locomotion")]
    [SerializeField] BaseAction<MoveData> _moveAction;
    MoveData _moveData;
    [TabGroup("Bounds")]
    [SerializeField] CheckXYDistanceAction _boundsXYDistanceAction;
    [TabGroup("Locomotion")]
    [SerializeField] CheckXYDistanceAction _waypointXYDistanceAction;
    [TabGroup("Locomotion")]
    [SerializeField] BaseAction<ActionCooldownData> _idleMovementAction;
    ActionCooldown _idleCooldown;
    [TabGroup("Sensors")]
    [SerializeField] protected WallSensorInfo _groundSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] WallSensorInfo _rightWallSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] WallSensorInfo _leftWallSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] WallSensorInfo _ceilingSensorInfo;
    [TabGroup("Locomotion")]
    [SerializeField] RotationAction _rotationPatrolAction;
    protected RotationActionData _rotationData;

    [TabGroup("Locomotion")]
    [SerializeField] RandomMovementSO _randomMovementSO;

    Vector2 _randomPoint;
    protected override void OnEnable()
    {
        base.OnEnable();
        RB.useGravity = false;
        _groundSensorInfo.SubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.SubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.SubscribeToEvents(SensorHandler);
        _ceilingSensorInfo.SubscribeToEvents(SensorHandler);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _nextWaypoint = _startingPointIndex;
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
        _rotationData = new RotationActionData(EnemyVisualHolder);
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
                    AnimatorHandler.Animator.SetFloat(AnimatorHelper.GetParameter(AnimatorParameterType.Speed), StatSheet.Speed);
                    returnBack = false;
                }
                else
                {
                    AnimatorHandler.Animator.SetFloat(AnimatorHelper.GetParameter(AnimatorParameterType.Speed), RB.velocity.x);
                    returnBack = true;
                    return true;
                }
                //set animation based on speed
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

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 target = new Vector2(_waypoints[_nextWaypoint].position.x, _waypoints[_nextWaypoint].position.y);
        var direction = GetNormilizedDirectionToTarget(position, target);

        bool moveToNextPoint = false;
        //check if need to move to next point
        //check if reached the next waypoint
        if (CheckWaypoint(transform.position, _waypoints[_nextWaypoint].position, true, out bool returnBack))
        {
            if (returnBack)
            {
                //Rotate(ZERO, _rotationPatrolAction);
                return;
            }
            moveToNextPoint = true;
        }
        else
        {
            Rotate(direction.x, _rotationPatrolAction);
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
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);
    }
    protected void Rotate(float directionX, RotationAction rotationAction)
    {
        if (directionX == 0)
        {
            _rotationData.UpdateRotationData(RotationDirectionType.Front);
        }
        else if (directionX > 0)
        {
            _rotationData.UpdateRotationData(RotationDirectionType.Right);
        }
        else if (directionX < 0)
        {
            _rotationData.UpdateRotationData(RotationDirectionType.Left);
        }
        rotationAction.InitAction(_rotationData);
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
    public override bool CheckKnockbackEnemy()
    {
        if (WaitAction(_knockbackDurationAction, ref _knockbackCooldown))
        {
            return true;
        }
        //Debug.Log("Knocking back enemy");
        var direction = GetNormilizedDirectionToTarget(MiddleOfBody.position, GameManager.Instance.PlayerManager.MiddleOfBody.position);
        direction *= -1;//Knock back away from player
        _moveData.UpdateData(new Vector3(direction.x, direction.y, ZERO), EnemyStatSheet.KnockbackSpeed);
        _moveAction.InitAction(_moveData);
        //RB.AddForce(new Vector3(direction* _knockbackForce, ZERO, ZERO),ForceMode.VelocityChange);
        return false;
    }
    protected override void TakeDamage()
    {
        base.TakeDamage();
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
#if UNITY_EDITOR
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
#endif
}