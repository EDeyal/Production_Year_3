using UnityEngine;

public class QovaxKnockbackState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        if (_qovax.CheckKnockbackEnemy())
        {
            return _qovaxStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        _qovax.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.IsHit));
    }
}
