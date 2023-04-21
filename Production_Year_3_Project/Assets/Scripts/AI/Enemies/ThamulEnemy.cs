using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class ThamulEnemy : GroundEnemy
{
    [TabGroup("General")]
    [SerializeField] ThamulStateHandler _thamulStateHandler;
    [TabGroup("General")]
    [SerializeField] CombatHandler _combatHandler;
    [TabGroup("Abilities")]
    [SerializeField] Attack _thamulProjectileAttack;
    [TabGroup("Abilities")]
    [SerializeField] GameObject _thamulProjectile;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _thamulMeleeDistance;
    ActionCooldown _projectileCooldown;
    [TabGroup("Abilities")]
    [SerializeField] BaseAction<ActionCooldownData> _projectileCooldownAction;
    public CheckDistanceAction ThamulMeleeDistance => _thamulMeleeDistance;

    public ThamulStateHandler ThamulStateHandler => StateHandler as ThamulStateHandler;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    public override void Awake()
    {
        base.Awake();
        CheckValidation();
        _thamulStateHandler.CheckValidation();
        _thamulStateHandler.CurrentState.EnterState();
        _combatHandler.Init();
        _projectileCooldown = new ActionCooldown();
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_thamulProjectileAttack==null)
            throw new System.Exception("ThamulEnemy has no projetile attack");

    }
    public bool Shoot()
    {
        var direction = GeneralFunctions.GetXDirectionToTarget(transform.position, GameManager.Instance.PlayerManager.transform.position);
        Rotate(direction, _rotationChaseAction);
        if (WaitAction(_projectileCooldownAction, ref _projectileCooldown))
        {
            Debug.Log("Shooting at player");
            ShootProjectile(_thamulProjectileAttack,direction);
            return true; //when completed return true
        }
        return false;
    }
    private void ShootProjectile(Attack givenAttack,int direction)
    {
        Projectile bullet = GameManager.Instance.ObjectPoolsHandler.ThamulProjectilePool.GetPooledObject();
        bullet.transform.position = MiddleOfBody.position;
        bullet.CacheStats(givenAttack, DamageDealer);
        if (direction == 1)
        {
            bullet.Blast(new Vector3(1, 0, 0));
        }
        else if (direction == -1)
        {
            bullet.Blast(new Vector3(-1, 0, 0));
        }
        else
        {
            Debug.LogWarning("Wrong Data from Thamul Shoot Projectile Direction");
        }
        bullet.gameObject.SetActive(true);
    }
    private void Update()
    {
        BaseState nextState = _thamulStateHandler.CurrentState.RunCurrentState();
        if (Damageable.CurrentHp <= 0)
        {
            nextState = _thamulStateHandler.DeathState;
        }
        if (_thamulStateHandler.CurrentState != nextState)
        {
            _thamulStateHandler.CurrentState.ExitState();
            _thamulStateHandler.CurrentState = nextState;
            _thamulStateHandler.CurrentState.EnterState();
        }
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
        ThamulMeleeDistance.DrawGizmos(MiddleOfBody.position);
    }
#endif
}
