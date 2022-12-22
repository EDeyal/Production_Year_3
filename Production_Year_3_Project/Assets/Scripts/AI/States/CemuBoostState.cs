using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuBoostState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("CemuBoostState");
        return _cemuStateHandler.CombatState;
    }
}
