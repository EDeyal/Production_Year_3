using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuPatrolState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        if (GeneralFunctions.IsInRange(transform.position, _cemuHandler.PlayerManager.transform.position, _cemuHandler.NoticePlayerDistance))
        {
            return _cemuHandler.CombatState;
        }
        ((GroundEnemy)_cemuHandler.RefEnemy).Patrol();
        return this;
    }
}
