using System;
using UnityEngine;

public class QovaxStateHandler : BaseStateHandler
{
    [SerializeField] BaseState _patrolState;
    public BaseState PatrolState => _patrolState;
    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_patrolState == null)
            throw new System.Exception("Patrol state is Null");
    }
}
