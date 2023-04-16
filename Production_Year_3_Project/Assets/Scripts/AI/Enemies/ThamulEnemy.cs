using Sirenix.OdinInspector;
using UnityEngine;

public class ThamulEnemy : GroundEnemy
{
    [TabGroup("General")]
    [SerializeField] ThamulStateHandler _thamulStateHandler;
    [TabGroup("General")]
    [SerializeField] CombatHandler _combatHandler;

    [TabGroup("Sensors")]
    [SerializeField] CheckDistanceAction _thamulMeleeDistance;
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
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
    }
    public bool Shoot()
    {
        var direction = GeneralFunctions.GetXDirectionToTarget(transform.position, GameManager.Instance.PlayerManager.transform.position);
        Rotate(direction, _rotationChaseAction);
        return true; //when completed return true
        Debug.Log("Shooting at player");
        //use ability
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
