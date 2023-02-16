public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Cemu Idle State");
        if (_cemu.NoticePlayerDistance.InitAction(new DistanceData(
            _cemu.MiddleOfBody.position, _cemuStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            //check if player is in sight
            if (_cemu.HasDirectLineToPlayer(_cemu.NoticePlayerDistance.Distance))
                return _cemuStateHandler.CombatState;
        }
        return _cemuStateHandler.PatrolState;
    }
}
