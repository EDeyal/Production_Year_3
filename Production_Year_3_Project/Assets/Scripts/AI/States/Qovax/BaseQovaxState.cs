using UnityEngine;

public abstract class BaseQovaxState : BaseState
{
    private void OnValidate()
    {
        if (!_qovaxStateHandler)
            _qovaxStateHandler = GetComponent<QovaxStateHandler>();
    }
    [SerializeField] protected QovaxStateHandler _qovaxStateHandler;
}
