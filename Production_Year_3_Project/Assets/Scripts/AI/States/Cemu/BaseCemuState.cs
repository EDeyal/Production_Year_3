using UnityEngine;
public abstract class BaseCemuState : BaseState
{

    [SerializeField] protected CemuStateHandler _cemuStateHandler;
    protected CemuEnemy _cemu;
    private void Awake()
    {
        _cemu = (CemuEnemy)_cemuStateHandler.RefEnemy;
    }
    private void OnValidate()
    {
        if (!_cemuStateHandler)
            _cemuStateHandler = GetComponent<CemuStateHandler>();
    }
}
