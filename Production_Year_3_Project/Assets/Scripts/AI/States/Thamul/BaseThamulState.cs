using UnityEngine;

public abstract class BaseThamulState : BaseState
{
    [SerializeField] protected ThamulStateHandler _thamulStateHandler;
    protected ThamulEnemy _thamul;
    private void Awake()
    {
        GetThamulRef();
    }
    private void OnValidate()
    {
        if (!_thamulStateHandler)
            _thamulStateHandler = GetComponent<ThamulStateHandler>();
    }
    public void GetThamulRef()
    {
        if(_thamul == null)
            _thamul = (ThamulEnemy)_thamulStateHandler.RefEnemy;
    }
}
