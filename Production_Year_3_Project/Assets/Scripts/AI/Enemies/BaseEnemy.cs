using JetBrains.Annotations;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : BaseCharacter, ICheckValidation
{
    protected const float ZERO = 0;
    protected const float ONE = 1;
    protected const float MINUS_ONE = -1;
    #region Fields
    [SerializeField] BoundHandler _boundHandler;
    [SerializeField] Rigidbody _rb;
    [SerializeField] SensorHandler _sensorHandler;
    [SerializeField] CheckDistanceAction _noticePlayerDistance;
    [SerializeField] CheckDistanceAction _chasePlayerDistance;
    [SerializeField] AnimatorHandler _animatorHandler;
    [SerializeField] EnemyStatSheet _enemyStatSheet;

    [Tooltip("Range does not change anything, Only change the offset of the center of the object")]
    [SerializeField] private RaycastSensor _playerSensor;
    #endregion

    #region Properties
    public Rigidbody RB => _rb;
    public SensorHandler SensorHandler => _sensorHandler;
    public CheckDistanceAction NoticePlayerDistance => _noticePlayerDistance;
    public CheckDistanceAction ChasePlayerDistance => _chasePlayerDistance;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public EnemyStatSheet EnemyStatSheet => _enemyStatSheet;
    public BoundHandler BoundHandler => _boundHandler;
    #endregion
    private void OnValidate()
    {
        _boundHandler.ValidateBounds();
    }
    public override void Awake()
    {
        base.Awake();
        StatSheet.InitializeStats();
    }
    public virtual void CheckValidation()
    {
        if (!_sensorHandler)
            throw new System.Exception("BaseEnemy has no SensorHandler");
        if (!_playerSensor.SensorTarget)
        {
            throw new System.Exception("BaseEnemy has no SensorTarget on player sensor");
        }
    }
    private void OnDrawGizmosSelected()
    {
        BoundHandler.DrawBounds();
        if (!GameManager.Instance)
            return;
        _playerSensor.DrawLineToTarget(transform,GameManager.Instance.PlayerManager.transform ,_noticePlayerDistance.Distance);
    }
    public bool HasDirectLineToPlayer(float maxDistanceToPlayer)
    {
        return _playerSensor.SendRayToTarget(transform,GameManager.Instance.PlayerManager.transform ,maxDistanceToPlayer);
    }
    protected bool WaitAction(BaseAction<ActionCooldownData> action, ref ActionCooldown cooldown)
    {
        if (action.InitAction(new ActionCooldownData(ref cooldown)))
        {
            return true;
        }
        return false;
    }
    public abstract void OnDeath();
}
