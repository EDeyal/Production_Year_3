using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuStateHandler : BaseStateHandler
{
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_patrolState == null)
            throw new System.Exception("Patrol state is Null");
    }
    [SerializeField] BaseState _patrolState;
    public BaseState PatrolState => _patrolState;
}
