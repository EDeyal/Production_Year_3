using Sirenix.OdinInspector;
using UnityEngine;
public class CemuEnemy : GroundEnemy
{
    [TabGroup("General")]
    [SerializeField] CemuStateHandler _cemuStateHandler;
    [TabGroup("Abilities")]
    [ReadOnly] bool _isBoostActive;
    ActionCooldown _beforeBoostCooldown;
    [TabGroup("Abilities")]
    [SerializeField] BaseAction<ActionCooldownData> _boostCooldownAction;
    [TabGroup("General")]
    [SerializeField] CombatHandler _combatHandler;
    [TabGroup("Abilities")]
    [SerializeField] Ability _cemuAbility;

    [TabGroup("Visuals")]
    [SerializeField] SkinnedMeshRenderer _meshRenderer;
    [TabGroup("Visuals")]
    [SerializeField][ReadOnly] Material _outlineMaterial;
    [TabGroup("Visuals")]
    [SerializeField,Range(0,1)]float _outlineAlpha;
    public bool IsBoostActive => _isBoostActive;
    public CemuStateHandler CemuStateHandler => StateHandler as CemuStateHandler;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetOutline(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
        EnemyStatSheet.DecayingHealth.onDecayingHealthReduce.AddListener(DecreacedDecayingHealth);
        _outlineMaterial = _meshRenderer.materials[1];
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (!_meshRenderer)
        {
            throw new System.Exception("Cemu Enemy has no mesh menderer assigned");
        }
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
    private void DecreacedDecayingHealth(float f)
    {
        if (StatSheet.DecayingHealth.CurrentDecayingHealth <= 0)
        {
            SetOutline(false);
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
                SetOutline(true);
            }
            return true;
        }

        _isBoostActive = false;
        return false;
    }
    private void SetOutline(bool isActivated)
    {
        var color = _outlineMaterial.color;
        if (isActivated)
        {
            color.a = _outlineAlpha;
            _outlineMaterial.color = color;
        }
        else
        {
            color.a = 0;
            _outlineMaterial.color = color;
        }
    }

    private void OnDestroy()
    {
        Effectable.OnStatusEffectRemoved.RemoveListener(RemoveBuffActivation);
        EnemyStatSheet.DecayingHealth.onDecayingHealthReduce.RemoveListener(DecreacedDecayingHealth);
    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
#if UNITY_EDITOR
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        ChasePlayerDistance.DrawGizmos(MiddleOfBody.position);
        NoticePlayerDistance.DrawGizmos(MiddleOfBody.position);
    }
#endif
}
