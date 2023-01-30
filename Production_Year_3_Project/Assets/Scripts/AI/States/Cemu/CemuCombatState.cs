public class CemuCombatState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("CemuCombatState");
        //check if is in combat range
        if (!_cemuStateHandler.RefEnemy.ChasePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            //not in range
            return _cemuStateHandler.IdleState;
        }
        var cemu = (CemuEnemy)_cemuStateHandler.RefEnemy;
        if (cemu.BoundsXDistanceAction.InitAction(new DistanceData(transform.position, cemu.BoundHandler.Bound.max))
    || cemu.BoundsXDistanceAction.InitAction(new DistanceData(transform.position, cemu.BoundHandler.Bound.min)))
        {
            return _cemuStateHandler.IdleState;
        }

        return _cemuStateHandler.ChaseState;
    }
}
