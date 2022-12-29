using Sirenix.OdinInspector;
using UnityEngine;
public class CemuEnemy : GroundEnemy
{
    [SerializeField] CemuStateHandler _cemuStateHandler;
    [ReadOnly] bool _isBoostActive;
    ActionCooldown _beforeBoostCooldown;
    [SerializeField] BaseAction<ActionCooldownData> _boostCooldownAction;

    //create script that will hold all damage functions "Combat Handler"
    [SerializeField] DamageDealingCollider _damageDealingCollider;
    [SerializeField] DamageDealer _damageDealer;
    [SerializeField] Damageable _damagable;
    [SerializeField] Attack _attack;

    public bool IsBoostActive => _isBoostActive;
    public void Awake()
    {
        _cemuStateHandler.CheckValidation();
        _cemuStateHandler.CurrentState.EnterState();
        _beforeBoostCooldown = new ActionCooldown();
        _damageDealingCollider.CacheReferences(_attack,_damageDealer);//add to combat handler
    }
    //visual -> controller
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
            return true;
        }
        _isBoostActive = false;
        return false;
    }

}
