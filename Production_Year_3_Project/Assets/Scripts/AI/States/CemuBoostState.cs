using UnityEngine;

public class CemuBoostState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("CemuBoostState");
        var enemy = (CemuEnemy)_cemuStateHandler.RefEnemy;
        if (enemy.CheckBoostActivation())
        {
            return _cemuStateHandler.ChaseState;
        }
        return _cemuStateHandler.CombatState;
    }
}
