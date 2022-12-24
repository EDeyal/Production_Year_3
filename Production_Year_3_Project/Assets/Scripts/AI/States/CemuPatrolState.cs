using UnityEngine;

public class CemuPatrolState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Cemu Patrol State");
        if (_cemuStateHandler.RefEnemy.NoticePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            ((GroundEnemy)_cemuStateHandler.RefEnemy).StopMovement();
            return _cemuStateHandler.CombatState;
        }
        ((GroundEnemy)_cemuStateHandler.RefEnemy).Patrol();
        return this;
    }
}
