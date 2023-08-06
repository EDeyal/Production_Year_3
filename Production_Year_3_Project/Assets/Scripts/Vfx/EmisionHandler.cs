using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;

public class EmisionHandler : MonoBehaviour, ICheckValidation
{
    [SerializeField] float _emissionOn = 5;
    [SerializeField] float _emissionOff = 0;
    [SerializeField] List<GameObject> _matObjects;
    private List<Renderer> _renderers = new List<Renderer>();
    [SerializeField] Color _color;
    [SerializeField] AnimationCurve _colorInEase;
    [SerializeField] AnimationCurve _colorOutEase;
    [SerializeField] float _transitionDuration = 3.5f;
    private void Awake()
    {
        CheckValidation();
        if (_matObjects != null)
        {
            //_renderers = new List<Renderer>();
            for (int i = 0; i < _matObjects.Count; i++)
            {
                Renderer renderer = _matObjects[i].GetComponent<Renderer>();
                _renderers.Add(renderer);
            }

        }
        if (_renderers == null)
        {
            Debug.LogError($"Save Point {gameObject.name} could not get renderer");
        }
    }
    public void Activate()
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.DOColor(_color * _emissionOn, "_EmissionColor", _transitionDuration).SetEase(_colorInEase);
        }
    }
    public void Deactivate()
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.DOColor(_color * _emissionOff, "_EmissionColor", _transitionDuration).SetEase(_colorOutEase);

        }
    }

    public void CheckValidation()
    {
        if (_matObjects.Count == 0)
            throw new System.Exception($"EmisionHandler {gameObject.name} has no mat object");
    }
}
