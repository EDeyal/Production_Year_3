public class ThamulShootState : BaseThamulState
{
    bool _isBeforeShoot = true;
    public override BaseState RunCurrentState()
    {
        if (_isBeforeShoot)
        {
            if(_thamul.Shoot())
            {
                _isBeforeShoot = false;
                _thamul.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked));
            }
        }
        else
        {
            if (_thamul.AfterShoot())
            {
                //_thamul.AnimatorHandler.Animator.SetBool(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked), false);
                _isBeforeShoot=true;
                _thamul.ResetProjectileCooldown();
                return _thamulStateHandler.CombatState;
            }
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _thamul.AnimatorHandler.Animator.SetTrigger(
            AnimatorHelper.GetParameter(AnimatorParameterType.Ranged));
        _thamul.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed), ZERO);
    }
    public override void ExitState()
    {
        base.ExitState();
        //_thamul.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.HasAttacked));
    }
}
