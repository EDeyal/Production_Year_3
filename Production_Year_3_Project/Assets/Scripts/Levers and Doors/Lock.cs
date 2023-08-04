using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lock : MonoBehaviour, ICheckValidation
{
    [SerializeField] Animator lockAnimator;
    [SerializeField] EmisionHandler _emisionHandler;


    private void Awake()
    {
        CheckValidation();
        _emisionHandler.Deactivate();
    }
    public void ActivateLock()
    {
        _emisionHandler.Activate();
        //trigger animator
    }
    public void CheckValidation()
    {
        if (_emisionHandler == null)
            throw new System.Exception($"Lock {gameObject.name} has no emision handler");
        if (lockAnimator == null)
            throw new System.Exception($"Lock {gameObject.name} has no animator");
    }
}
