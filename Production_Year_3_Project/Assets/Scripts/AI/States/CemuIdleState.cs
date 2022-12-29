using UnityEngine;
public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Cemu Idle State");
        if (_cemuStateHandler.RefEnemy.NoticePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            return _cemuStateHandler.CombatState;
        }
        return _cemuStateHandler.PatrolState;
    }
}
