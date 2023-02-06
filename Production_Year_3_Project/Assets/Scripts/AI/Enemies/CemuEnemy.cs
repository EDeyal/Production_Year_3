using Sirenix.OdinInspector;
using UnityEngine;
public class CemuEnemy : GroundEnemy
{
    [SerializeField] CemuStateHandler _cemuStateHandler;
    [ReadOnly] bool _isBoostActive;
    ActionCooldown _beforeBoostCooldown;
    [SerializeField] BaseAction<ActionCooldownData> _boostCooldownAction;
    [SerializeField] CombatHandler _combatHandler;
    [SerializeField] Ability _cemuAbility;
    public bool IsBoostActive => _isBoostActive;

    protected override void OnDisable()
    {
        _isBoostActive = false;
    }
    public override void Awake()
    {
        base.Awake();
        CheckValidation();
        _cemuStateHandler.CheckValidation();
        _cemuStateHandler.CurrentState.EnterState();
        _beforeBoostCooldown = new ActionCooldown();
        _combatHandler.Init();
        Effectable.OnStatusEffectRemoved.AddListener(RemoveBuffActivation);
    }

    private void Update()
    {
        BaseState nextState = _cemuStateHandler.CurrentState.RunCurrentState();
        if (Damageable.CurrentHp <= 0)
        {
            nextState = _cemuStateHandler.DeathState;
        }
        if (_cemuStateHandler.CurrentState != nextState)
        {
            _cemuStateHandler.CurrentState.ExitState();
            _cemuStateHandler.CurrentState = nextState;
            _cemuStateHandler.CurrentState.EnterState();
        }
    }
    private void RemoveBuffActivation(StatusEffect boost)
    {
        if (boost is MovementSpeedBoost)
        {
            _isBoostActive = false;
            _cemuStateHandler.CurrentState.UpdateState();
        }
    }
    public bool CheckBoostActivation()
    {
        if (WaitAction(_boostCooldownAction, ref _beforeBoostCooldown))
        {
            if (!_isBoostActive)
            {
                _isBoostActive = true;
                _cemuAbility.Cast(this);
            }
            return true;
        }
        _isBoostActive = false;
        return false;
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        ChasePlayerDistance.DrawGizmos(transform.position);
        NoticePlayerDistance.DrawGizmos(transform.position);
    }
    private void OnDestroy()
    {
        Effectable.OnStatusEffectRemoved.RemoveListener(RemoveBuffActivation);
    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
}
