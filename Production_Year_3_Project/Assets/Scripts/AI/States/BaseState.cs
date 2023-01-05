using System;
using UnityEngine;
using UnityEngine.Events;
public abstract class BaseState :MonoBehaviour
{
    protected const float ONE = 1;
    protected const float ZERO = 0;

    public event Action OnStateEnter;
    public event Action OnStateExit;
    public abstract BaseState RunCurrentState();

    public virtual void EnterState()
    {
        OnStateEnter?.Invoke();
    }
    public virtual void ExitState()
    {
        OnStateExit?.Invoke();
    }
    public virtual void UpdateState()
    {

    }

}
