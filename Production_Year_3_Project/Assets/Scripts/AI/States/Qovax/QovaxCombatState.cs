using UnityEngine;

public class QovaxCombatState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {        
        if (_qovax.ChasePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.RefEnemy.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            Debug.Log("Qovax Combat State");
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
