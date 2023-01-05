using Sirenix.OdinInspector;
using UnityEngine;
public class CemuEnemy : GroundEnemy
{
    [SerializeField] CemuStateHandler _cemuStateHandler;
    [ReadOnly] bool _isBoostActive;
    ActionCooldown _beforeBoostCooldown;
    [SerializeField] BaseAction<ActionCooldownData> _boostCooldownAction;
    [SerializeField] CombatHandler _combatHandler;

    public bool IsBoostActive => _isBoostActive;
    public void Awake()
    {
        CheckValidation();
        _cemuStateHandler.CheckValidation();
        _cemuStateHandler.CurrentState.EnterState();
        _beforeBoostCooldown = new ActionCooldown();
        _combatHandler.Init();
    }
    private void Update()
    {
        BaseState nextState = _cemuStateHandler.CurrentState.RunCurrentState();

        if (_cemuStateHandler.CurrentState != nextState)
        {
            _cemuStateHandler.CurrentState.ExitState();
            _cemuStateHandler.CurrentState = nextState;
            _cemuStateHandler.CurrentState.EnterState();
        }
    }
    public bool CheckBoostActivation()
    {

        if (_boostCooldownAction.InitAction(new ActionCooldownData(ref _beforeBoostCooldown)))
        {
            _isBoostActive = true;
            
            Effectable.ApplyStatusEffect(new CemuSpeedBoost(), Effector);
            //add status effect for decaying health
            return true;
        }
        _isBoostActive = false;
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        ChasePlayerDistance.DrawGizmos(transform.position);
        NoticePlayerDistance.DrawGizmos(transform.position);
    }
}
