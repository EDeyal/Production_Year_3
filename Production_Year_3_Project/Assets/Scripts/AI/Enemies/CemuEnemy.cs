using UnityEngine;

public class CemuEnemy : GroundEnemy
{
    CemuStateHandler _cemuStateHandler;
    public void Awake()
    {
        _cemuStateHandler.CheckValidation();
        _cemuStateHandler.CurrentState.OnStateEnter?.Invoke();
    }
    //visual -> controller
    private void Update()
    {
        BaseState nextState = _cemuStateHandler.CurrentState.RunCurrentState();

        if (_cemuStateHandler.CurrentState != nextState)
        {
            _cemuStateHandler.CurrentState.OnStateExit?.Invoke();
            _cemuStateHandler.CurrentState = nextState;
            _cemuStateHandler.CurrentState.OnStateEnter?.Invoke();
        }

        

    }
}
