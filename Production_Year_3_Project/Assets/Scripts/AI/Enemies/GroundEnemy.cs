using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class GroundEnemy : BaseEnemy
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
    [TabGroup("Locomotion")]
    [SerializeField] RotationAction _rotationPatrolAction;
    [TabGroup("Locomotion")]
    [SerializeField] protected RotationAction _rotationChaseAction;
    RotationActionData _rotationData;
    [TabGroup("Bounds")]
    [SerializeField] CheckXDistanceAction _boundsXDistanceAction;
    [TabGroup("Locomotion")]
    [SerializeField] CheckXDistanceAction _waypointXDistanceAction;
    [TabGroup("Sensors")]
    [SerializeField] GroundSensorInfo _groundSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] WallSensorInfo _rightWallSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] WallSensorInfo _leftWallSensorInfo;
    [TabGroup("Sensors")]
    [SerializeField] LedgeSensorInfo _ledgeSensorInfo;


    public CheckXDistanceAction BoundsXDistanceAction => _boundsXDistanceAction;
    public override void Awake()
    {
        base.Awake();
        _rotationData = new RotationActionData(EnemyVisualHolder);
    }
    protected override void OnEnable()
    {
        _nextWaypoint = _startingPointIndex;
        RB.useGravity = true;
        base.OnEnable();
        _groundSensorInfo.SubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.SubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.SubscribeToEvents(SensorHandler);
        _ledgeSensorInfo.SubscribeToEvents(SensorHandler);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _groundSensorInfo.UnsubscribeToEvents(SensorHandler);
        _rightWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _leftWallSensorInfo.UnsubscribeToEvents(SensorHandler);
        _ledgeSensorInfo.UnsubscribeToEvents(SensorHandler);
    }
    private void Start()
    {
        _moveData = new MoveData(RB, Vector3.zero, EnemyStatSheet.Speed);
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
        //move first and then check surrounding
        float direction = 0;
        direction = GetDirection(direction);
        Rotate(direction,_rotationPatrolAction);
        _moveData.UpdateData(new Vector3(direction, ZERO, ZERO), EnemyStatSheet.Speed);
        _moveAction.InitAction(_moveData);

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
        if (_rightWallSensorInfo.IsNearWall && direction > 0)
        {
            moveToNextPoint = true;
        }
        if (_leftWallSensorInfo.IsNearWall && direction < 0)
        {
            moveToNextPoint = true;
        }
        //if needed to turn for some reason, turn to next waypoint
        if (moveToNextPoint)
        {
            IsMovingToNextPoint();
        }
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
    float GetDirection(float direction)
    {
        if (_waypoints[_nextWaypoint].position.x > transform.position.x)
        {
            return ONE;
        }
        else if (_waypoints[_nextWaypoint].position.x < transform.position.x)
        {
            return MINUS_ONE;
        }
        else
        {
            return ZERO;
        }
    }
    public virtual void StopMovement()
    {
        _moveData.UpdateData(ZERO);
        _moveAction.InitAction(_moveData);
    }
    public virtual void Chase()
    {
        //find player and determin his direction
        var direction = GeneralFunctions.GetXDirectionToTarget(transform.position, GameManager.Instance.PlayerManager.transform.position);
        Rotate(direction, _rotationChaseAction);
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
    public override bool CheckKnockbackEnemy()
    {
        if (WaitAction(_knockbackDurationAction, ref _knockbackCooldown))
        {
            return true;
        }
        Debug.Log("Knocking back enemy");
        var direction = GeneralFunctions.GetXDirectionToTarget(MiddleOfBody.position, GameManager.Instance.PlayerManager.MiddleOfBody.position);
        direction *= -1;//Knock back away from player
        _moveData.UpdateData(new Vector3(direction, ZERO, ZERO), EnemyStatSheet.KnockbackSpeed);
        _moveAction.InitAction(_moveData);
        //RB.AddForce(new Vector3(direction* _knockbackForce, ZERO, ZERO),ForceMode.VelocityChange);
        return false;
    }
#if UNITY_EDITOR
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
#endif
}
