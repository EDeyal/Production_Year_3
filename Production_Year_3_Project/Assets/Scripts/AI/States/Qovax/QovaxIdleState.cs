public class QovaxIdleState : BaseQovaxState
{

    public override BaseState RunCurrentState()
    {
        var enemyRef = _qovaxStateHandler.RefEnemy;
        //if in notice range
        if (enemyRef.NoticePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            //if in direct line of sight enter combat
            if (enemyRef.HasDirectLineToPlayer(enemyRef.NoticePlayerDistance.Distance))
                return _qovaxStateHandler.CombatState;
        }
        if (((FlyingEnemy)enemyRef).IdleWaitAction())//wait a little bit before moving to the next point
        {
            return _qovaxStateHandler.PatrolState;
        }
        return this;//stay in idle
    }
}
