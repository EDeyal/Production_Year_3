using UnityEngine;
using UnityEngine.Events;
public abstract class BaseState :MonoBehaviour
{
    public UnityEvent OnStateEnter;
    public UnityEvent OnStateExit;
    public abstract BaseState RunCurrentState();
}
