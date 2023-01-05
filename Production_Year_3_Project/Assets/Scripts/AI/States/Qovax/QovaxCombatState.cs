using UnityEngine;

public class QovaxCombatState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        var enemyRef = _qovaxStateHandler.RefEnemy;

        if (enemyRef.ChasePlayerDistance.InitAction(new DistanceData(
            _qovaxStateHandler.RefEnemy.transform.position, _qovaxStateHandler.PlayerManager.transform.position)))
        {
            Debug.Log("Player in qovax chase range");
        }
        return _qovaxStateHandler.IdleState;
    }
}
