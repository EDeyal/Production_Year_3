using UnityEngine;

public class ThamulStateHandler : BaseStateHandler
{
    [SerializeField] BaseState _patrolState;
    [SerializeField] BaseState _chaseState;
    [SerializeField] BaseState _shootState;
    [SerializeField] BaseState _meleeState;
    public BaseState PatrolState => _patrolState;
    public BaseState ChaseState => _chaseState;
    public BaseState ShootState => _shootState;
    public BaseState MeleeState => _meleeState;
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_patrolState == null)
            throw new System.Exception("Patrol state is Null");
        if (_chaseState == null)
            throw new System.Exception("Chase state is Null");
        if (_shootState == null)
            throw new System.Exception("Shoot state is Null");
        if (_meleeState == null)
            throw new System.Exception("Melee state is Null");
    }
}
