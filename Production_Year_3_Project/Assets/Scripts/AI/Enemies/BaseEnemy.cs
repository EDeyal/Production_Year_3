using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : BaseCharacter, ICheckValidation
{
    protected const float ZERO = 0;
    protected const float ONE = 1;
    protected const float MINUS_ONE = -1;
    private float _rbStartingDrag;
    #region Fields

    [TabGroup("Bounds")]
    [SerializeField] BoundHandler _boundHandler;
    [TabGroup("Locomotion")]
    [SerializeField] Rigidbody _rb;
    [TabGroup("Visuals")]
    [SerializeField] GameObject _enemyVisualHolder;
    [TabGroup("Sensors")]
    [SerializeField] SensorHandler _sensorHandler;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _noticePlayerDistance;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _chasePlayerDistance;
    [TabGroup("Visuals")]
    [SerializeField] AnimatorHandler _animatorHandler;
    [TabGroup("Abilities")]
    [SerializeField] Ability _droppedAbilityForPlayer;
    [TabGroup("General")]
    [SerializeField] Collider _damageDealingCollider;
    [TabGroup("Locomotion")]
    [SerializeField] GameObject _startingPosition;
    [TabGroup("General")]
    [SerializeField] BaseStateHandler _stateHandler;

    [TabGroup("Sensors")]
    [Tooltip("Range does not change anything, Only change the offset of the center of the object")]
    [SerializeField] private RaycastSensor _playerSensor;

    [TabGroup("General")]
    [SerializeField] BaseAction<ActionCooldownData> _deathAction;
    ActionCooldown _deathCooldown;

    [TabGroup("Locomotion")]
    [SerializeField] protected BaseAction<ActionCooldownData> _knockbackDurationAction;
    protected ActionCooldown _knockbackCooldown;
    [TabGroup("Locomotion")]
    [SerializeField, ReadOnly] protected bool _isReceivingKnockback;
    #endregion

    #region Properties
    public Rigidbody RB => _rb;
    public SensorHandler SensorHandler => _sensorHandler;
    public CheckDistanceAction NoticePlayerDistance => _noticePlayerDistance;
    public CheckDistanceAction ChasePlayerDistance => _chasePlayerDistance;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public EnemyStatSheet EnemyStatSheet => StatSheet as EnemyStatSheet;
    public BoundHandler BoundHandler => _boundHandler;
    public Ability DroppedAbilityForPlayer => _droppedAbilityForPlayer;
    public GameObject EnemyVisualHolder => _enemyVisualHolder;
    public Collider DamageDealingCollider => _damageDealingCollider;
    public BaseStateHandler StateHandler => _stateHandler;

    #endregion
#if UNITY_EDITOR
    private void OnValidate()
    {
        _boundHandler.ValidateBounds();
    }
#endif
    public override void Awake()
    {
        base.Awake();
        StatSheet.InitializeStats();
        _deathCooldown = new ActionCooldown();
        _rbStartingDrag = RB.drag;
        _knockbackCooldown = new ActionCooldown();
        _isReceivingKnockback = true;
    }
    public virtual void CheckValidation()
    {
        if (!_sensorHandler)
            throw new System.Exception("BaseEnemy has no SensorHandler");
        if (!_playerSensor.SensorTarget)
            throw new System.Exception("BaseEnemy has no SensorTarget on player sensor");
        if (!EnemyVisualHolder)
            throw new System.Exception("Enemy has no Visual Holder");
        if (!_knockbackDurationAction)
            throw new System.Exception("Base Enemy has no knockbackAction");
    }
    private void Update()
    {
        BaseState nextState = _stateHandler.CurrentState.RunCurrentState();
        if (Damageable.CurrentHp <= 0)
        {
            nextState = _stateHandler.DeathState;
        }
        UpdateStateMachine(nextState);
    }
    protected void UpdateStateMachine(BaseState nextState)
    {
        if (_stateHandler.CurrentState != nextState)
        {
            _stateHandler.CurrentState.ExitState();
            _stateHandler.CurrentState = nextState;
            _stateHandler.CurrentState.EnterState();
        }
    }
#if UNITY_EDITOR
    public virtual void OnDrawGizmosSelected()
    {
        BoundHandler.DrawBounds();
        if (!GameManager.Instance)
            return;
        _playerSensor.DrawLineToTarget(MiddleOfBody, GameManager.Instance.PlayerManager.MiddleOfBody, _noticePlayerDistance.Distance);
    }
#endif
    public bool HasDirectLineToPlayer(float maxDistanceToPlayer)
    {
        return _playerSensor.SendRayToTarget(MiddleOfBody, GameManager.Instance.PlayerManager.MiddleOfBody, maxDistanceToPlayer);
    }
    protected bool WaitAction(BaseAction<ActionCooldownData> action, ref ActionCooldown cooldown)
    {
        if (action.InitAction(new ActionCooldownData(ref cooldown)))
        {
            return true;
        }
        return false;
    }
    public virtual bool CheckForCooldown(BaseAction<ActionCooldownData> action, ActionCooldown cooldown)
    {
        if (WaitAction(action, ref cooldown))
        {
            return true;
        }
        return false;
    }
    public virtual void OnDeath()
    {
        _damageDealingCollider.gameObject.SetActive(false);
        //can add logic until destroyed cooldown is completed
        if (_deathAction.InitAction(new ActionCooldownData(ref _deathCooldown)))
        {
            //can add logic to frame of death
            transform.gameObject.SetActive(false);
        }
    }
    protected virtual void OnEnable()
    {
        RB.velocity = Vector3.zero;
        RB.drag = _rbStartingDrag;
        _damageDealingCollider.gameObject.SetActive(true);
        transform.position = _startingPosition.transform.position;
        StatSheet.InitializeStats();
        Damageable.ResetParameters();
        Damageable.OnTakeDmgGFX.AddListener(TakeDamage);
    }
    protected virtual void OnDisable()
    {
        Damageable.OnTakeDmgGFX.RemoveListener(TakeDamage);
    }
    public virtual bool CheckKnockbackEnemy()
    {
        return false;
    }
    protected virtual void TakeDamage()
    {
        if (_isReceivingKnockback)
        {
            UpdateStateMachine(_stateHandler.KnockBackState);
        }
    }
}
