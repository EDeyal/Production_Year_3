public class CemuKnockbackState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        if (_cemu.CheckKnockbackEnemy())
        {
            return _cemuStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        _cemu.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.IsHit));
    }
}
