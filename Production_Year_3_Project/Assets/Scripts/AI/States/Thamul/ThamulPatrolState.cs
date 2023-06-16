using UnityEngine;

public class ThamulPatrolState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("ThamulPatrolState");
        if (_thamul.NoticePlayerDistance.InitAction(new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            if (_thamul.HasDirectLineToPlayer(_thamul.NoticePlayerDistance.Distance))
            {
                _thamul.StopMovement();
                return _thamulStateHandler.CombatState;
            }
        }
        _thamul.Patrol();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _thamul.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            _thamul.EnemyStatSheet.Speed);
        //animations
    }
    public override void ExitState()
    {
        base.ExitState();
        _thamul.AnimatorHandler.Animator.SetFloat(
    AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
    _thamul.EnemyStatSheet.Speed);
        //animations
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _thamul.AnimatorHandler.Animator.SetFloat(
AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
_thamul.EnemyStatSheet.Speed);
    }
}
