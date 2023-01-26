using System;
using UnityEngine;

public class QovaxStateHandler : BaseStateHandler
{
    [SerializeField] BaseState _patrolState;
    [SerializeField] BaseState _evasionState;
    [SerializeField] BaseState _chargeState;
    [SerializeField] BaseState _fatigueState;
    public BaseState PatrolState => _patrolState;
    public BaseState EvasionState => _evasionState;
    public BaseState ChargeState => _chargeState;
    public BaseState FatigueState => _fatigueState;

    public override void CheckValidation()
    {
        base.CheckValidation();
        if (_patrolState == null)
            throw new System.Exception("Patrol state is Null");
    }
}
