public class CemuPatrolState : BaseCemuState
{

    public override BaseState RunCurrentState()
    {
        var enemyRef = _cemuStateHandler.RefEnemy;
        //Debug.Log("Cemu Patrol State");
        if (enemyRef.NoticePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            if (enemyRef.HasDirectLineToPlayer(enemyRef.NoticePlayerDistance.Distance))
            {
                ((GroundEnemy)enemyRef).StopMovement();
                return _cemuStateHandler.CombatState;
            }
        }
        ((GroundEnemy)enemyRef).Patrol();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);
        //add logic for animat
    }
    public override void ExitState()
    {
        base.ExitState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
    AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
    _cemuStateHandler.RefEnemy.EnemyStatSheet.Speed);

    }
}
