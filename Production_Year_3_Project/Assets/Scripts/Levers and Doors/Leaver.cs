using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leaver : DamageableObject
{
    [SerializeField] bool _isActivated = false;
    public bool IsActivated => _isActivated;
    [SerializeField] Animator leaverAnimator;
    [SerializeField] public direction directionEnum;

    [SerializeField] private UnityEvent onLeverActivated;
    public override void Awake()
    {
        base.Awake();
        Damageable.OnDeath.AddListener(ActivateLeaver);
        Damageable.OnDeath.AddListener(ActivateEvent);
    }
    private void OnDestroy()
    {
        Damageable.OnDeath.RemoveListener(ActivateLeaver);
        Damageable.OnDeath.RemoveListener(ActivateEvent);
    }

    public void ActivateEvent()
    {
        onLeverActivated?.Invoke();
    }

    public void ActivateLeaver()
    {

        Debug.Log("Leaver Activated");
        switch (directionEnum)
        {
            case direction.left:
                leaverAnimator.SetTrigger("MoveLeft");
                _isActivated = true;
                break;
            case direction.right:
                leaverAnimator.SetTrigger("MoveRight");
                _isActivated = true;
                break;
        }
    }
    public enum direction
    {
        left,
        right
    }
}
