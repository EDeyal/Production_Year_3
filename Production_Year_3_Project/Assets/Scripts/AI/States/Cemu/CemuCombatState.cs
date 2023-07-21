using UnityEngine;
public class CemuCombatState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("CemuCombatState");
        //check if is in combat range
        if (!_cemu.ChasePlayerDistance.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemuStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            //not in range
            return _cemuStateHandler.IdleState;
        }
        if (_cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.max))
    || _cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.min)))
        {
            Debug.Log("Cemu returning to Idle");

            return _cemuStateHandler.IdleState;
        }
        return _cemuStateHandler.ChaseState;
    }
}
