using UnityEngine;

public class QovaxEnemy : FlyingEnemy
{
    [SerializeField] QovaxStateHandler _qovaxStateHandler;
    [SerializeField] CombatHandler _combatHandler;
    ActionCooldown _actionCooldown;

    [SerializeField] BaseAction<ActionCooldownData> _evasionCooldownAction;
    [SerializeField] BaseAction<ActionCooldownData> _chargeCooldownAction;
    [SerializeField] BaseAction<ActionCooldownData> _fatigueCooldownAction;
    protected Vector3 _chargePoint;
    bool _isCharging;
    bool _isFatigued;
    public bool IsFatigued { get => _isFatigued; set => _isFatigued = value; }
    public QovaxStatSheet QovaxStatSheet => StatSheet as QovaxStatSheet;

    protected override void OnEnable()
    {
        base.OnEnable();
        _isCharging = false;
        _isFatigued=false;
    }
    public override void Awake()
    {
        base.Awake();
        CheckValidation();
        _combatHandler.Init();
        _actionCooldown = new ActionCooldown();
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_combatHandler == null)
            throw new System.Exception("QovaxEnemy has no Combat Handler");
        if (!_evasionCooldownAction)
            throw new System.Exception("QovaxEnemy has no evasion Cooldown Action");
        if (!_chargeCooldownAction)
            throw new System.Exception("QovaxEnemy has no charge Cooldown Action");
        if (!_fatigueCooldownAction)
            throw new System.Exception("QovaxEnemy has no fatigue Cooldown Action");
    }
    private void Update()
    {

        BaseState nextState = _qovaxStateHandler.CurrentState.RunCurrentState();
        if (Damageable.CurrentHp <= 0)
        {
            nextState = _qovaxStateHandler.DeathState;
        }
        if (_qovaxStateHandler.CurrentState != nextState)
        {
            _qovaxStateHandler.CurrentState.ExitState();
            _qovaxStateHandler.CurrentState = nextState;
            _qovaxStateHandler.CurrentState.EnterState();
        }
    }
    private bool CheckForChargeCooldown()
    { 
        return CheckForCooldown(_chargeCooldownAction,_actionCooldown);
    }
    public bool Charge()
    {
        //base.Charge();
        //If charge point is ZERO Update charge point
        if (_chargePoint == Vector3.zero)//is not set
        {
            //find player location
            //Debug.Log("Assigning New Point");
            _chargePoint = GameManager.Instance.PlayerManager.transform.position;
        }
        if (!_isCharging)
        {
             if (CheckForChargeCooldown())//cooldown before attack
            {
                //Start Movement after cooldown
                _isCharging = true;
                Debug.Log("Charging");
                AnimatorHandler.Animator.SetFloat(AnimatorHelper.GetParameter(AnimatorParameterType.Speed), QovaxStatSheet.ChargeSpeed);
            }
            else
            {
                //if still in cooldown return back and wait for next frame
                return false;
            }
        }
        //--------
        //add can't be staggered or stunned but can still take damage
        //--------
        if (CheckBounds())
        {
            Debug.Log("Hit Bounds, Returning");
            //incase reached bounds you stop
            ResetCharge();
            return true;
        }

        if (CheckWalls())
        {
            //Debug.Log("Hit walls, Returning");
            //incase reached Walls you stop
            ResetCharge();
            return true;
        }

        if (CheckWaypoint(transform.position,_chargePoint,false,out bool returnBack))
        {
            //Debug.Log("Reached destination");
            //when reached destination return true
            ResetCharge();
            return true;
        }

        //charge Logic
        MoveTo(_chargePoint,QovaxStatSheet.ChargeSpeed);
        return false;
    }
    public void ResetCharge()
    {
        _isCharging = false;
        _chargePoint = Vector3.zero;
    }
    public bool CheckForEvasionCooldown()
    {
        //Evasion Logic
        return CheckForCooldown(_evasionCooldownAction, _actionCooldown);
    }
    public bool CheckForFatigueCooldown()
    {
        //Fatige Logic
        return CheckForCooldown(_fatigueCooldownAction, _actionCooldown);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        ChasePlayerDistance.DrawGizmos(transform.position);
        NoticePlayerDistance.DrawGizmos(transform.position);
    }
    public void CheckFatigued()
    {
        if (!_isFatigued)
        {
            ResetLookAt();
            _isFatigued = true;
            _qovaxStateHandler.RefEnemy.Effectable.ApplyStatusEffect(new MovementSpeedBoost());
            //add slow boost
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();
        if (_groundSensorInfo.IsNearWall)
        {
            AnimatorHandler.Animator.SetBool(AnimatorHelper.GetParameter(AnimatorParameterType.IsGrounded), true);
        }
    }
}
