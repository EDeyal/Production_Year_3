using UnityEngine;
public class ThamulIdleState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Thamul Idle State");
        if (_thamul.NoticePlayerDistance.InitAction(new DistanceData(
            _thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            //check if player is in sight
            if (_thamul.HasDirectLineToPlayer(_thamul.NoticePlayerDistance.Distance))
                return _thamulStateHandler.CombatState;

        }
        return _thamulStateHandler.PatrolState;
    }
}
