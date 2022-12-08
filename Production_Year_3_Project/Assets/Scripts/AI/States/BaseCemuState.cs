using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCemuState : BaseState
{
    protected CemuStateHandler _cemuHandler;
    private void Start()
    {
        if(!_cemuHandler)
            _cemuHandler = GetComponent<CemuStateHandler>();
    }
}
