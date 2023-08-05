using UnityEngine;

public class ThamulKnockbackState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("ThamulKnockBack");
        if (_thamul.CheckKnockbackEnemy())
        {
            return _thamulStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        //hit
        _thamul.AnimatorHandler.Animator.SetTrigger(AnimatorHelper.GetParameter(AnimatorParameterType.IsHit));
        var randomRange = Random.Range(0, 1);
        if (randomRange == 1)
        {
            _thamul.AnimatorHandler.Animator.SetTrigger(
                AnimatorHelper.GetParameter(AnimatorParameterType.HitUp));
        }
        else
        {
            _thamul.AnimatorHandler.Animator.SetTrigger(
                AnimatorHelper.GetParameter(AnimatorParameterType.HitDown));
        }
    }
}
