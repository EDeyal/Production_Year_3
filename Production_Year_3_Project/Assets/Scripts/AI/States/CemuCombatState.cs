using UnityEngine;
public class CemuCombatState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("CemuCombatState");
        //check if is in combat range
        if (!_cemuStateHandler.RefEnemy.chasePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            //not in range
            return _cemuStateHandler.IdleState;
        }

        var cemu = (CemuEnemy)_cemuStateHandler.RefEnemy;

        if (cemu.IsBoostActive)
        {
            return _cemuStateHandler.ChaseState;
        }
        else
        {
            return _cemuStateHandler.BoostState;
        }
    }
}
