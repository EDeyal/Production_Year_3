using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuChaseState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("CemuChaseState");
        return _cemuStateHandler.CombatState;
    }
}
