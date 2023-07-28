using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuDeathState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        _cemu.OnDeath();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemu.AnimatorHandler.Animator.SetTrigger(
    AnimatorHelper.GetParameter(AnimatorParameterType.IsDead));
        GameManager.Instance.SoundManager.PlaySound("CemuDeathSoundTest");
    }
}
