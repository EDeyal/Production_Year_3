using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    /// <summary>
    /// Invoked when this object deals damage
    /// </summary>
    public UnityEvent<DamageHandler> OnDealDamage;
    /// <summary>
    /// Invoked when this object hits an attack
    /// </summary>
    public UnityEvent<Attack> OnHitAttack;
    /// <summary>
    /// Invoked when this object is done adding damage mods
    /// </summary>
    public UnityEvent<DamageHandler> OnDoneDamageCalc;
    
    



}
