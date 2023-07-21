using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : DamageableObject
{
    [SerializeField] bool _isActivated = false;
    public bool IsActivated => _isActivated;
    [SerializeField] Animator leaverAnimator;
    [SerializeField] public direction directionEnum;

    public override void Awake()
    {
        base.Awake();
        Damageable.OnDeath.AddListener(ActivateLeaver);
    }
    private void OnDestroy()
    {
        Damageable.OnDeath.RemoveListener(ActivateLeaver);
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
