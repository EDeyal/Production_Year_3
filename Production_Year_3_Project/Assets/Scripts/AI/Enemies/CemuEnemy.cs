using UnityEngine;
public class CemuEnemy : GroundEnemy
{
    [SerializeField] CemuStateHandler _cemuStateHandler;
    bool _isBoostActive;
    public bool IsBoostActive => _isBoostActive;
    public void Awake()
    {
        _cemuStateHandler.CheckValidation();
        _cemuStateHandler.CurrentState.EnterState();
    }
    //visual -> controller
    private void Update()
    {
        BaseState nextState = _cemuStateHandler.CurrentState.RunCurrentState();

        if (_cemuStateHandler.CurrentState != nextState)
        {
            _cemuStateHandler.CurrentState.ExitState();
            _cemuStateHandler.CurrentState = nextState;
            _cemuStateHandler.CurrentState.EnterState();
        }
    }
}
