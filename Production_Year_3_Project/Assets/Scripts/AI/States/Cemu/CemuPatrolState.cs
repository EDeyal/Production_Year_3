public class CemuPatrolState : BaseCemuState
{

    public override BaseState RunCurrentState()
    {
        //Debug.Log("Cemu Patrol State");
        if (_cemu.NoticePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            if (_cemu.HasDirectLineToPlayer(_cemu.NoticePlayerDistance.Distance))
            {
                _cemu.StopMovement();
                return _cemuStateHandler.CombatState;
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
