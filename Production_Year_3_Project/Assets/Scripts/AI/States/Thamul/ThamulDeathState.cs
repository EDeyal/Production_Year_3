using UnityEngine;

public class ThamulDeathState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("ThamulDeathState");
        _thamul.OnDeath();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        //animations
    }
}
