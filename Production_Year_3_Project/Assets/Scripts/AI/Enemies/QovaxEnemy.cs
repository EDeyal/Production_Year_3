using UnityEngine;

public class QovaxEnemy : FlyingEnemy
{
    [SerializeField] QovaxStateHandler _qovaxStateHandler;
    [SerializeField] CombatHandler _combatHandler;
    ActionCooldown _actionCooldown;

    [SerializeField] BaseAction<ActionCooldownData> _evasionCooldownAction;
    [SerializeField] BaseAction<ActionCooldownData> _chargeCooldownAction;
    [SerializeField] BaseAction<ActionCooldownData> _fatigueCooldownAction;

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
    private void OnDrawGizmosSelected()
    {
        ChasePlayerDistance.DrawGizmos(transform.position);
        NoticePlayerDistance.DrawGizmos(transform.position);
    }
    public bool CheckForChargeCooldown()
    {
        //charge Logic
        return CheckForCooldown(_chargeCooldownAction,_actionCooldown);
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

}
