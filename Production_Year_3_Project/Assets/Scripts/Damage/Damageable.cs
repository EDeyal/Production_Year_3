using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StatSheet))]
public class Damageable : MonoBehaviour
{
     private float currentHp;
     private float maxHp;


    [SerializeField] private TargetType targetType;

    /// <summary>
    /// Invoked when this object gets hit with an attack
    /// </summary>
    public UnityEvent<Attack> OnGetHit;
    /// <summary>
    /// Invoked when this object takes damage
    /// </summary>
    public UnityEvent<DamageHandler> OnTakeDamage;
    /// <summary>
    /// Invoked when this object is done adding damage mods
    /// </summary>
    public UnityEvent<DamageHandler> OnTotalDamageCalcRecieve;
    /// <summary>
    /// Invoked when this object is healed
    /// </summary>
    public UnityEvent<DamageHandler> OnGetHealed;
    /// <summary>
    /// Invoked when this object takes leathal damage
    /// </summary>
    public UnityEvent OnDeath;



    public TargetType TargetType { get => targetType;}
    public float CurrentHp { get => currentHp;}
    public float MaxHp { get => maxHp; }

    public void SetStats(StatSheet stats)
    {
        maxHp = stats.MaxHp;
        currentHp = maxHp;
    }

    public virtual void GetHit(Attack givenAttack)
    {
        OnGetHit?.Invoke(givenAttack);
        TakeDamage(givenAttack.DamageHandler);
    }

    public virtual void GetHit(Attack givenAttack, DamageDealer givenDealer)
    {
        OnGetHit?.Invoke(givenAttack);
        givenDealer.OnHitAttack?.Invoke(givenAttack);
        TakeDamage(givenAttack.DamageHandler, givenDealer);
    }

    public virtual void TakeDamage(DamageHandler givenDamage)
    {
        OnTakeDamage?.Invoke(givenDamage);
        OnTotalDamageCalcRecieve?.Invoke(givenDamage);
        currentHp -= givenDamage.GetFinalMult();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
    }

    public virtual void TakeDamage(DamageHandler givenDamage, DamageDealer givenDamageDealer)
    {
        OnTakeDamage?.Invoke(givenDamage);
        givenDamageDealer.OnDealDamage?.Invoke(givenDamage);
        OnTotalDamageCalcRecieve?.Invoke(givenDamage);
        givenDamageDealer.OnTotalDamageCalcDeal?.Invoke(givenDamage);

        currentHp -= givenDamage.GetFinalMult();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
    }


    public virtual void Heal(DamageHandler givenDamage)
    {
        OnGetHealed?.Invoke(givenDamage);
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }
}

public enum TargetType
{
    Player,
    Enemy, 
    Terrain
}
