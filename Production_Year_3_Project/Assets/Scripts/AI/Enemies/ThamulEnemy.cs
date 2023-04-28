using Sirenix.OdinInspector;
using System.Collections;
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
    [SerializeField] Attack _thamulMeleeAttack;
    [TabGroup("Abilities")]
    [SerializeField] GameObject _thamulProjectile;
    [TabGroup("Sensors")]
    [SerializeField] float _hightDifferenceOffset;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _thamulMeleeDistance;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _thamulRunAfterPlayerDistance;
    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _thamulRangeChaseDistance;
    ActionCooldown _projectileCooldown;
    ActionCooldown _meleeCooldown;
    [TabGroup("Abilities")]
    [SerializeField] BaseAction<ActionCooldownData> _projectileCooldownAction;
    [TabGroup("Abilities")]
    [SerializeField] BaseAction<ActionCooldownData> _meleeCooldownAction;
    [TabGroup("Abilities")]
    [SerializeField] Transform _rightAttack;
    [TabGroup("Abilities")]
    [SerializeField] Transform _leftAttack;
    [TabGroup("Abilities")]
    [SerializeField] float _attackRadius;
    [TabGroup("Abilities")]
    [SerializeField] LayerMask _hitLayer;
    [TabGroup("Abilities")]
    [SerializeField] bool _showChaseRanges = true;
    [TabGroup("Abilities")]
    [SerializeField] bool _showAttackRanges = true;
    [TabGroup("Abilities")]
    [SerializeField] bool _showAttackAreas = true;

    public CheckDistanceAction ThamulMeleeDistance => _thamulMeleeDistance;
    public CheckDistanceAction ThamulRunAfterPlayerDistance => _thamulRunAfterPlayerDistance;
    public CheckDistanceAction ThamulRangeChaseDistance => _thamulRangeChaseDistance;
    public ThamulStateHandler ThamulStateHandler => StateHandler as ThamulStateHandler;
    public float HightDifferenceOffset => _hightDifferenceOffset;

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
        _meleeCooldown = new ActionCooldown();
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_thamulProjectileAttack == null)
            throw new System.Exception("ThamulEnemy has no projetile attack");
        if (_thamulMeleeAttack == null)
            throw new System.Exception("ThamulEnemy has no melee attack");
    }
    private void ThamulRotate(out int direction)
    {
        direction = GeneralFunctions.GetXDirectionToTarget(transform.position, GameManager.Instance.PlayerManager.transform.position);
        Rotate(direction, _rotationChaseAction);
    }
    public bool MeleeAttack()
    {
        ThamulRotate(out int direction);
        if (WaitAction(_meleeCooldownAction, ref _meleeCooldown))
        {
            Debug.Log("Attacking with melee");
            if (direction == 1)
            {
                //right attack
                Attack(_rightAttack.position);
                StartCoroutine(ResetAttack());
            }
            else if (direction == -1)
            {
                //left attack
                Attack(_leftAttack.position);
                StartCoroutine(ResetAttack());
            }
            //if player is on thamul he will get damaged by the collider
            return true;
        }
        return false;
    }
    private void Attack(Vector3 attackPos)
    {
        Collider[] collidersFound = Physics.OverlapSphere(attackPos, _attackRadius, _hitLayer);
        foreach (Collider item in collidersFound)
        {
            BaseCharacter target = item.GetComponent<BaseCharacter>();
            if (ReferenceEquals(target, null) || target is not PlayerManager)//target is null or not player, return
            {
                return;
            }
            if (HasDirectLineToPlayer(_attackRadius))//has direct line to the player -> Attack
            {
                target.Damageable.GetHit(_thamulProjectileAttack, DamageDealer);//attack the player
            }
        }
    }
    IEnumerator ResetAttack()
    {
        yield return null;

    }
    public bool Shoot()
    {
        ThamulRotate(out int direction);

        if (WaitAction(_projectileCooldownAction, ref _projectileCooldown))
        {
            Debug.Log("Shooting at player");
            ShootProjectile(_thamulMeleeAttack, direction);
            return true; //when completed return true
        }
        return false;
    }
    public void ResetProjectileCooldown()
    {
        _projectileCooldown.ResetCooldown();
    }
    private void ShootProjectile(Attack givenAttack, int direction)
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
        if (_showChaseRanges)
        {
            ChasePlayerDistance.DrawGizmos(MiddleOfBody.position);
            NoticePlayerDistance.DrawGizmos(MiddleOfBody.position);

        }
        if (_showAttackRanges)
        {
            ThamulMeleeDistance.DrawGizmos(MiddleOfBody.position);
            ThamulRunAfterPlayerDistance.DrawGizmos(MiddleOfBody.position);
            ThamulRangeChaseDistance.DrawGizmos(MiddleOfBody.position);
        }
        if (_showAttackAreas)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_leftAttack.position, _attackRadius);
            Gizmos.DrawWireSphere(_rightAttack.position, _attackRadius);
        }
    }
#endif
}
