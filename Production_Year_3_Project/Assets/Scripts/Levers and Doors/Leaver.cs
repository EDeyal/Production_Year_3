using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : DamageableObject
{
    [SerializeField] bool _isActivated = false;
    public bool IsActivated => _isActivated;
    [SerializeField] Animation leaverAnimation;

    public override void Awake()
    {
        base.Awake();
        Damageable.OnDeath.AddListener(ActivateLeaver);//when player is dead it probably removes the asset from the list
    }
    private void OnDestroy()
    {
        Damageable.OnDeath.RemoveListener(ActivateLeaver);
    }
    public void ActivateLeaver()
    {
        //if interacted or hit
        //Play Animation
    }
}
