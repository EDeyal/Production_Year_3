using UnityEngine;

public class QovaxCombatState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {        
        if (_qovax.ChasePlayerDistance.InitAction(new DistanceData(
            _qovax.MiddleOfBody.position, _qovaxStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            //Debug.Log("Qovax Combat State");
            //During X seconds
            if (_qovax.CheckForEvasionCooldown())
            {
                //after time is over
                //move to charge state
                return _qovaxStateHandler.ChargeState;
            }
            else
            {
                //move to evasion state
                return _qovaxStateHandler.EvasionState;
            }
            
        }
        return _qovaxStateHandler.IdleState;
    }
}
