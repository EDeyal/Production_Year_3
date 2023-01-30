using UnityEngine;
public class QovaxDeathState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Qovax Death State");
        _qovax.OnDeath();
        return this;
    }
}
