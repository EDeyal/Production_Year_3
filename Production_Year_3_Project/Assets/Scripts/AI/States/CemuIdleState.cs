using UnityEngine;
public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        if (GeneralFunctions.IsInRange(transform.position, _cemuHandler.PlayerManager.transform.position, _cemuHandler.NoticePlayerDistance))
        {
            return _cemuHandler.CombatState;
        }
        return _cemuHandler.PatrolState;
    }
}
