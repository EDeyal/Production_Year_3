using UnityEngine;

public abstract class BaseThamulState : BaseState
{
    [SerializeField] protected ThamulStateHandler _thamulStateHandler;
    protected ThamulEnemy _thamul;
    private void Awake()
    {
        _thamul = (ThamulEnemy)_thamulStateHandler.RefEnemy;
    }
    private void OnValidate()
    {
        if (!_thamulStateHandler)
            _thamulStateHandler = GetComponent<ThamulStateHandler>();
    }
}
