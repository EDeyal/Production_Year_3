using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuDeathState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        _cemuStateHandler.RefEnemy.OnDeath();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemu.AnimatorHandler.Animator.SetBool(
    AnimatorHelper.GetParameter(AnimatorParameterType.IsDead),true);
    }
}
