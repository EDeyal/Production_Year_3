using UnityEngine;
public class CemuStateHandler : BaseStateHandler
{
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_patrolState == null)
            throw new System.Exception("Patrol state is Null");
        if (_chaseState == null)
            throw new System.Exception("Chase state is Null");
        if (_boostState == null)
            throw new System.Exception("Boost state is Null");
    }

    [SerializeField] BaseState _patrolState;
    [SerializeField] BaseState _chaseState;
    [SerializeField] BaseState _boostState;
    public BaseState PatrolState => _patrolState;
    public BaseState ChaseState => _chaseState;
    public BaseState BoostState => _boostState;
}
