using UnityEngine;

public class QovaxEnemy : FlyingEnemy
{
    [SerializeField] QovaxStateHandler _qovaxStateHandler;
    [SerializeField] CombatHandler _combatHandler;

    public override void Awake()
    {
        base.Awake();
        CheckValidation();
        _combatHandler.Init();
    }
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_combatHandler == null)
            throw new System.Exception("QovaxEnemy has no Combat Handler");
    }
    private void Update()
    {

        BaseState nextState = _qovaxStateHandler.CurrentState.RunCurrentState();
        if (Damageable.CurrentHp <= 0)
        {
            nextState = _qovaxStateHandler.DeathState;
        }
        if (_qovaxStateHandler.CurrentState != nextState)
        {
            _qovaxStateHandler.CurrentState.ExitState();
            _qovaxStateHandler.CurrentState = nextState;
            _qovaxStateHandler.CurrentState.EnterState();
        }

    }
    private void OnDrawGizmosSelected()
    {
        ChasePlayerDistance.DrawGizmos(transform.position);
        NoticePlayerDistance.DrawGizmos(transform.position);
    }
}
