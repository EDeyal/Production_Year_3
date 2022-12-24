using UnityEngine;
public abstract class BaseCemuState : BaseState
{
    private void OnValidate()
    {
        if (!_cemuStateHandler)
            _cemuStateHandler = GetComponent<CemuStateHandler>();
    }
    [SerializeField] protected CemuStateHandler _cemuStateHandler;
}
