using UnityEngine;

public class CemuPatrolState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Cemu Patrol State");
        if (GeneralFunctions.IsInRange(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position, _cemuStateHandler.NoticePlayerDistance))
        {
            return _cemuStateHandler.CombatState;
        }
        ((GroundEnemy)_cemuStateHandler.RefEnemy).Patrol();
        return this;
    }
}
