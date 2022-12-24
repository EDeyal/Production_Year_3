using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuChaseState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("CemuChaseState");
        ((GroundEnemy)_cemuStateHandler.RefEnemy).Chase();
        return _cemuStateHandler.CombatState;
    }
}
