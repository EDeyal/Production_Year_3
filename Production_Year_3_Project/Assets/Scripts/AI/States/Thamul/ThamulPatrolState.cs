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
        //animations
    }
    public override void ExitState()
    {
        base.ExitState();
        //animations
    }
}
