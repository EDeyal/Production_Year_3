public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        var enemyRef = _cemuStateHandler.RefEnemy;
        //Debug.Log("Cemu Idle State");
        if (enemyRef.NoticePlayerDistance.InitAction(new DistanceData(
            _cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            //check if player is in sight
            if (enemyRef.HasDirectLineToPlayer(enemyRef.NoticePlayerDistance.Distance))
                return _cemuStateHandler.CombatState;
        }
        return _cemuStateHandler.PatrolState;
    }
}
