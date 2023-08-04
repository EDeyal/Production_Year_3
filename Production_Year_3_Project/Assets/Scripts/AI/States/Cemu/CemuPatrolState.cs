using UnityEngine;
public class CemuPatrolState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        if (!_cemu.IsBoostActive)
        {
            _cemu.RemoveBoostParticles();
        }
        if (_cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.max))
|| _cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.min)))
        {
            _cemu.Patrol();
            return this;
        }
        if (_cemu.IsPlayerInBounds(_cemuStateHandler.PlayerManager.MiddleOfBody))
         {

            //Debug.Log("Cemu Patrol State");
            if (_cemu.NoticePlayerDistance.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemuStateHandler.PlayerManager.MiddleOfBody.position)))
            {
                if (_cemu.HasDirectLineToPlayer(_cemu.NoticePlayerDistance.Distance))
                {
                    _cemu.StopMovement();
                    return _cemuStateHandler.CombatState;
                }
            }
        }
        _cemu.Patrol();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemu.AnimatorHandler.Animator.SetFloat(
        AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
        _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
    public override void ExitState()
    {
        base.ExitState();
        _cemu.AnimatorHandler.Animator.SetFloat(
    AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
    _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        _cemu.AnimatorHandler.Animator.SetFloat(
        AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
        _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
    }
}
