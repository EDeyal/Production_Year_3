using UnityEngine;

public abstract class BaseQovaxState : BaseState
{
    protected QovaxEnemy _qovax;
    
    private void Awake()
    {
        _qovax = (QovaxEnemy)_qovaxStateHandler.RefEnemy;
    }
    private void OnValidate()
    {
        if (!_qovaxStateHandler)
            _qovaxStateHandler = GetComponent<QovaxStateHandler>();
    }
    [SerializeField] protected QovaxStateHandler _qovaxStateHandler;
}
