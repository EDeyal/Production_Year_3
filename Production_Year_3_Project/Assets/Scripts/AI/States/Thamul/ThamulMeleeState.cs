public class ThamulMeleeState : BaseThamulState
{
    bool _isBeforeAttack = true;
    bool _isTransitioningOut = false;
    public override BaseState RunCurrentState()
    {
        if (_isBeforeAttack)
        {
            if (_thamul.MeleeAttack())//will wait until finishing the attack
            {
                _isBeforeAttack = false;
            }
        }
        else
        {
            if (_isTransitioningOut)
            {
                if (_thamul.MeleeTransitionOut())//wait to transiton
                {
                    _isBeforeAttack = true;
                    _thamul.ResetMeleeCooldown();
                    _isTransitioningOut = false;
                    return _thamulStateHandler.CombatState;
                }
            }
            else
            {
                if (_thamul.AfterMelee())//wait until animation finished
                {
                    _thamul.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked));
                    _thamul.ResetMeleeCooldown();
                    _isTransitioningOut = true;
                }
            }
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
    public override void ExitState()
    {
        base.ExitState();
        //_thamul.AnimatorHandler.Animator.SetBool(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked), false);
        //_thamul.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked));

    }
}