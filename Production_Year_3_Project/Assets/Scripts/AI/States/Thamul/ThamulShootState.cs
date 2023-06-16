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
            }
            return this;
        }
        else
        {
            if (_thamul.AfterShoot())
            {
                _isBeforeShoot=true;
                _thamul.ResetProjectileCooldown();
                return _thamulStateHandler.CombatState;
            }
            else
                return this;
        }

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
    }
}
