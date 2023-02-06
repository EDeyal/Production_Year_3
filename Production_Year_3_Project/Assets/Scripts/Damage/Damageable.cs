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
    public UnityEvent<Attack, Damageable> OnGetHit;
    /// <summary>
    /// Invoked when this object takes damage
    /// </summary>
    public UnityEvent<Attack, Damageable> OnTakeDamage;
    /// <summary>
    /// Invoked when this object is done adding damage mods
    /// </summary>
    public UnityEvent<Attack, Damageable> OnTotalDamageCalcRecieve;
    /// <summary>
    /// Invoked when this object is healed
    /// </summary>
    public UnityEvent<DamageHandler, Damageable> OnGetHealed;
    /// <summary>
    /// Invoked when this object takes leathal damage
    /// </summary>
    public UnityEvent OnDeath;
    public UnityEvent OnTakeDmgGFX;
    public UnityEvent OnHealGFX;



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

    public void ResetParameters()
    {
        Heal(new DamageHandler() { BaseAmount = maxHp });
    }

    public virtual void GetHit(Attack givenAttack)
    {
        if (!_canReciveDamage || currentHp <= 0 || !givenAttack.CheckTargetValidity(targetType))
            return;
        OnGetHit?.Invoke(givenAttack, this);
        TakeDamage(givenAttack);
    }

    public virtual void GetHit(Attack givenAttack, DamageDealer givenDealer)
    {
        if (!_canReciveDamage || currentHp <= 0 || !givenAttack.CheckTargetValidity(targetType))
            return;

        Debug.Log(gameObject.name + " was hit by " + givenDealer.name);
        OnGetHit?.Invoke(givenAttack, this);
        givenDealer.OnHitAttack?.Invoke(givenAttack);
        TakeDamage(givenAttack, givenDealer);
    }

    public virtual void TakeDamage(Attack givenAttack)
    {
        OnTakeDamage?.Invoke(givenAttack, this);
        OnTotalDamageCalcRecieve?.Invoke(givenAttack, this);
        float finalAmount = givenAttack.DamageHandler.GetFinalMult();
        finalAmount = ReduceDecayingHealth(finalAmount);
        currentHp -= finalAmount;

        OnTakeDmgGFX?.Invoke();
        givenAttack.DamageHandler.ClearModifiers();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
        if (_canReciveDamage)
            StartCoroutine(InvulnerabilityPhase());
    }

    public virtual void TakeDamage(Attack givenAttack, DamageDealer givenDamageDealer)
    {
        OnTakeDamage?.Invoke(givenAttack, this);
        givenDamageDealer.OnDealDamage?.Invoke(givenAttack.DamageHandler);
        OnTotalDamageCalcRecieve?.Invoke(givenAttack, this);
        givenDamageDealer.OnTotalDamageCalcDeal?.Invoke(givenAttack.DamageHandler);
        float finalAmount = givenAttack.DamageHandler.GetFinalMult();
        finalAmount = ReduceDecayingHealth(finalAmount);
        currentHp -= finalAmount;

        OnTakeDmgGFX?.Invoke();

        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            givenDamageDealer.OnKill?.Invoke(this, givenAttack.DamageHandler);
        }
        ClampHp();
        if (_canReciveDamage)
            StartCoroutine(InvulnerabilityPhase());
    }


    public virtual void Heal(DamageHandler givenDamage)
    {
        OnGetHealed?.Invoke(givenDamage, this);
        OnHealGFX?.Invoke();
    }

    private float ReduceDecayingHealth(float finalDamage)
    {
        float currentDecayingHealth = owner.StatSheet.DecayingHealth.CurrentDecayingHealth;
        owner.StatSheet.DecayingHealth.CurrentDecayingHealth -= finalDamage;
        owner.StatSheet.DecayingHealth.onDecayingHealthReduce?.Invoke(finalDamage);
        owner.StatSheet.DecayingHealth.ClampHealth();
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
