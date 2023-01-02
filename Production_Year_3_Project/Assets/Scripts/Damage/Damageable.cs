using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StatSheet))]
public class Damageable : MonoBehaviour
{
    [SerializeField, ReadOnly] private float currentHp;
    [SerializeField, ReadOnly] private float maxHp;
    [SerializeField, ReadOnly] private float invulnerabilityDuration;
    protected bool _canReciveDamage = true;
    private BaseCharacter owner;

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



    public TargetType TargetType { get => targetType; }
    public float CurrentHp { get => currentHp; }
    public float MaxHp { get => maxHp; }
    public BaseCharacter Owner { get => owner; }

    public void CacheOwner(BaseCharacter givenCharacter)
    {
        owner = givenCharacter;
    }

    public void SetStats(StatSheet stats)
    {
        maxHp = stats.MaxHp;
        currentHp = maxHp;
        invulnerabilityDuration = stats.InvulnerabilityDuration;
    }

    public virtual void GetHit(Attack givenAttack)
    {
        if (!_canReciveDamage)
            return;
        OnGetHit?.Invoke(givenAttack);
        TakeDamage(givenAttack.DamageHandler);
    }

    public virtual void GetHit(Attack givenAttack, DamageDealer givenDealer)
    {
        if (!_canReciveDamage)
            return;
        OnGetHit?.Invoke(givenAttack);
        givenDealer.OnHitAttack?.Invoke(givenAttack);
        TakeDamage(givenAttack.DamageHandler, givenDealer);
    }

    public virtual void TakeDamage(DamageHandler givenDamage)
    {
        OnTakeDamage?.Invoke(givenDamage);
        OnTotalDamageCalcRecieve?.Invoke(givenDamage);
        float finalAmount = givenDamage.GetFinalMult();
        finalAmount = ReduceDecayingHealth(finalAmount);
        currentHp -= finalAmount;
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
        if (_canReciveDamage)
            StartCoroutine(InvulnerabilityPhase());
    }

    public virtual void TakeDamage(DamageHandler givenDamage, DamageDealer givenDamageDealer)
    {
        OnTakeDamage?.Invoke(givenDamage);
        givenDamageDealer.OnDealDamage?.Invoke(givenDamage);
        OnTotalDamageCalcRecieve?.Invoke(givenDamage);
        givenDamageDealer.OnTotalDamageCalcDeal?.Invoke(givenDamage);
        float finalAmount = givenDamage.GetFinalMult();
        finalAmount =  ReduceDecayingHealth(finalAmount);
        currentHp -= finalAmount;
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            givenDamageDealer.OnKill?.Invoke(this, givenDamage);
        }
        ClampHp();
        if (_canReciveDamage)
            StartCoroutine(InvulnerabilityPhase());
    }


    public virtual void Heal(DamageHandler givenDamage)
    {
        OnGetHealed?.Invoke(givenDamage);
    }
    
    private float ReduceDecayingHealth(float finalDamage)
    {
        float currentDecayingHealth = owner.StatSheet.DecayingHealth.CurrentDecayingHealth;
        owner.StatSheet.DecayingHealth.CurrentDecayingHealth -= finalDamage;
        finalDamage -= currentDecayingHealth;
        return finalDamage;
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    IEnumerator InvulnerabilityPhase()
    {
        _canReciveDamage = false;
        yield return new WaitForSeconds(invulnerabilityDuration);
        _canReciveDamage = true;
    }
}

public enum TargetType
{
    Player,
    Enemy,
    Terrain
}
