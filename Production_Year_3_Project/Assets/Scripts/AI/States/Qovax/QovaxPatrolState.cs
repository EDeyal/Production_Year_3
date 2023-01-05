using UnityEngine;
public class QovaxPatrolState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Patrol State");
        var enemyRef = _qovaxStateHandler.RefEnemy;
        //if player is in range move to combat state if not keep patrolling
        if (enemyRef.NoticePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.RefEnemy.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            if (enemyRef.HasDirectLineToPlayer(enemyRef.NoticePlayerDistance.Distance))
            {
                return _qovaxStateHandler.CombatState;
            }
        }
        ((FlyingEnemy)enemyRef).Patrol();
        return this;
    }
}
