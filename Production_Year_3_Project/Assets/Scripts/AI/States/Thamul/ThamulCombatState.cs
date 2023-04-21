using UnityEngine;
public class ThamulCombatState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("ThamulCombatState");
        //check if is in combat Range
        if (!_thamul.ChasePlayerDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position,_thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.IdleState;
        }
        //check if in bounds
        if (_thamul.BoundsXDistanceAction.InitAction(new DistanceData(_thamul.MiddleOfBody.position,_thamul.BoundHandler.Bound.max))
            ||_thamul.BoundsXDistanceAction.InitAction(new DistanceData(_thamul.MiddleOfBody.position,_thamul.BoundHandler.Bound.min)))
        {
            return _thamulStateHandler.IdleState;
        }
        //check if in melee range -> chase
        if (_thamul.ThamulMeleeDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position,_thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.ChaseState;
        }
        //check if in ranged range -> shoot
        return _thamulStateHandler.ShootState;
    }
}
