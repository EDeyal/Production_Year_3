using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimatorHandler
{
    [SerializeField] Animator _animator;
    public Animator Animator => _animator;
}
