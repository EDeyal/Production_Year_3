using UnityEngine;
public class QovaxIdleState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Idle State");
        //if in notice range
        if (_qovax.NoticePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            //if in direct line of sight enter combat
            if (_qovax.HasDirectLineToPlayer(_qovax.NoticePlayerDistance.Distance))
                return _qovaxStateHandler.CombatState;
        }
        if (_qovax.IdleWaitAction())//wait a little bit before moving to the next point
        {
            return _qovaxStateHandler.PatrolState;
        }
        return this;//stay in idle
    }
}
