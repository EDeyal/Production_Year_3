using UnityEngine;
public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Cemu Idle State");
        if (GeneralFunctions.IsInRange(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position, _cemuStateHandler.NoticePlayerDistance))
        {
            return _cemuStateHandler.CombatState;
        }
        return _cemuStateHandler.PatrolState;
    }
}
