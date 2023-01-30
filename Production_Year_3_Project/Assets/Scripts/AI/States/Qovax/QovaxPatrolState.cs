using UnityEngine;
public class QovaxPatrolState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Qovax Patrol State");
        //if player is in range move to combat state if not keep patrolling
        if (_qovax.NoticePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.RefEnemy.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            if (_qovax.HasDirectLineToPlayer(_qovax.NoticePlayerDistance.Distance))
            {
                return _qovaxStateHandler.CombatState;
            }
        }
        _qovax.Patrol();
        return this;
    }
}
