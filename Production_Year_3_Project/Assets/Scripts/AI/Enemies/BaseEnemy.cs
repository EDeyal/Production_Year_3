using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : BaseCharacter, ICheckValidation
{
    protected const float ZERO = 0;
    protected const float ONE = 1;
    protected const float MINUS_ONE = -1;
    private float _rbStartingDrag;
    #region Fields
    [SerializeField] BoundHandler _boundHandler;
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject _enemyVisualHolder;
    [SerializeField] SensorHandler _sensorHandler;
    [SerializeField] CheckDistanceAction _noticePlayerDistance;
    [SerializeField] CheckDistanceAction _chasePlayerDistance;
    [SerializeField] AnimatorHandler _animatorHandler;
    [SerializeField] Ability _droppedAbilityForPlayer;
    [SerializeField] Collider _damageDealingCollider;
    [SerializeField] GameObject _startingPosition;


    [Tooltip("Range does not change anything, Only change the offset of the center of the object")]
    [SerializeField] private RaycastSensor _playerSensor;

    [SerializeField] BaseAction<ActionCooldownData> _deathAction;
    ActionCooldown _deathCooldown;

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
    }
    public virtual void CheckValidation()
    {
        if (!_sensorHandler)
            throw new System.Exception("BaseEnemy has no SensorHandler");
        if (!_playerSensor.SensorTarget)
        {
            throw new System.Exception("BaseEnemy has no SensorTarget on player sensor");
        }
        if (!EnemyVisualHolder)
        {
            throw new System.Exception("Enemy has no Visual Holder");
        }
    }
#if UNITY_EDITOR
    public virtual void OnDrawGizmosSelected()
    {
        BoundHandler.DrawBounds();
        if (!GameManager.Instance)
            return;
        _playerSensor.DrawLineToTarget(transform, GameManager.Instance.PlayerManager.transform, _noticePlayerDistance.Distance);
    }
#endif
    public bool HasDirectLineToPlayer(float maxDistanceToPlayer)
    {
        return _playerSensor.SendRayToTarget(transform, GameManager.Instance.PlayerManager.transform, maxDistanceToPlayer);
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
    }
}
