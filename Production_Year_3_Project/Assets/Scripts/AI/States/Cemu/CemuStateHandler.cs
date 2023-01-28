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
    }

    [SerializeField] BaseState _patrolState;
    [SerializeField] BaseState _chaseState;
    public BaseState PatrolState => _patrolState;
    public BaseState ChaseState => _chaseState;
}
