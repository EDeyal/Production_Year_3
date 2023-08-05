using UnityEngine;
using UnityEngine.UIElements;

public class ThamulCombatState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("ThamulCombatState");
        //check if is in combat Range
        Vector3 thamulMiddleOfBodyPos = _thamul.MiddleOfBody.position;
        Vector3 playerMiddleOfBodyPos = _thamulStateHandler.PlayerManager.MiddleOfBody.position;
        if (!_thamul.ChasePlayerDistance.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, playerMiddleOfBodyPos)))
        {
            return _thamulStateHandler.IdleState;
        }
        //check if in bounds
        if (_thamul.BoundsXDistanceAction.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, _thamul.BoundHandler.Bound.max))
            ||_thamul.BoundsXDistanceAction.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, _thamul.BoundHandler.Bound.min)))
        {
            return _thamulStateHandler.IdleState;
        }
        //check if in melee range -> attack
        if (_thamul.ThamulMeleeDistance.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, playerMiddleOfBodyPos)))
        {
            return _thamulStateHandler.MeleeState;
        }
        //check if in RunAfterPlayer -> chase
        else if (_thamul.ThamulRunAfterPlayerDistance.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, playerMiddleOfBodyPos)))
        {
            return _thamulStateHandler.ChaseState;
        }
        //in ranged range -> shoot
        else if (_thamul.ThamulRangeChaseDistance.InitAction(
            new DistanceData(thamulMiddleOfBodyPos, playerMiddleOfBodyPos)))
        {
            return _thamulStateHandler.ShootState;
        }
        //player to far from range, go chase it
        else
        {
            return _thamulStateHandler.ChaseState;
        }
    }
}
