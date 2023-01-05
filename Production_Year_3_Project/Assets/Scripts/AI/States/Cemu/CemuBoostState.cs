using UnityEngine;

public class CemuBoostState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("CemuBoostState");
        var enemy = (CemuEnemy)_cemuStateHandler.RefEnemy;
        if (enemy.CheckBoostActivation())
        {
            return _cemuStateHandler.ChaseState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            ZERO);
    }
    public override void ExitState()
    {
        base.ExitState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
    AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
    _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
}
