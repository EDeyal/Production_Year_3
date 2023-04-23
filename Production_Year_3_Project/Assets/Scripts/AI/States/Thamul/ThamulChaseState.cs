using UnityEngine;

public class ThamulChaseState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Thamul Chase State");
        //if the enemy is within melee range
        if (_thamul.ThamulMeleeDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.MeleeState;
        }
        //else if player is within the chase range, chase it
        else if (_thamul.ThamulRunAfterPlayerDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            _thamul.Chase();
            return this;
        }
        //check for range attacks
        if (_thamul.ThamulRangeChaseDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.ShootState;
        }
        else if(_thamul.ChasePlayerDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            _thamul.Chase();
            return this;
        }
        //go back to see if you need to take a different action
        return _thamulStateHandler.CombatState;
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
