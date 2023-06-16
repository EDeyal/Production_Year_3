public class ThamulMeleeState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        if (_thamul.MeleeAttack())//will wait until finishing the attack
        {
            return _thamulStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _thamul.AnimatorHandler.Animator.SetTrigger(
            AnimatorHelper.GetParameter(AnimatorParameterType.Melee));
        _thamul.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed), ZERO);
    }
}