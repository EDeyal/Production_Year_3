using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuChaseState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //if boost is not active
        Debug.Log("CemuChaseState");
        if (!_cemu.IsBoostActive)
        {
            _cemu.CheckBoostActivation();
        }
        _cemu.Chase();
        return _cemuStateHandler.CombatState;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
    public override void ExitState()
    {
        base.ExitState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
}
