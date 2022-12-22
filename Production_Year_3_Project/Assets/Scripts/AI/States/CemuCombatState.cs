using UnityEngine;
public class CemuCombatState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("CemuCombatState");
        //check if boost is active
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
