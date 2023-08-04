using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lock : MonoBehaviour
{
    [SerializeField] Animator lockAnimator;
    private static float _emissionOn = 1;
    private static float _emissionOff = 0;
    [SerializeField] GameObject _LockMatObject;
    private Renderer _LockRenderer;
    [SerializeField] Color _LockColor;
    [SerializeField] AnimationCurve _colorInEase;
    [SerializeField] AnimationCurve _colorOutEase;
    [SerializeField] float _transitionDuration = 3.5f;
    private void Awake()
    {
        _LockRenderer = _LockMatObject.GetComponent<Renderer>();
    }
    public void ActivateLock()
    {
        _LockRenderer.material.DOColor(_LockColor * _emissionOn, "_EmissionColor", _transitionDuration).SetEase(_colorInEase);
    }
}
