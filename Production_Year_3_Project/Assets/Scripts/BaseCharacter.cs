using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [TabGroup("General")]
    [SerializeField] private DamageDealer damageDealer;
    [TabGroup("General")]
    [SerializeField] private Damageable damageable;
    [TabGroup("General")]
    [SerializeField] private StatusEffectable effectable;
    [TabGroup("General")]
    [SerializeField] private StatusEffector effector;
    [TabGroup("General")]
    [SerializeField] private StatSheet statSheet;
    [TabGroup("Abilities")]
    [SerializeField] private List<Boost> boosts = new List<Boost>();
    [TabGroup("Visuals")]
    [SerializeField] private Transform middleOfBody;
    public DamageDealer DamageDealer { get => damageDealer; }
    public Damageable Damageable { get => damageable; }
    public StatusEffectable Effectable { get => effectable; }
    public StatusEffector Effector { get => effector; }
    public StatSheet StatSheet { get => statSheet; }
    public List<Boost> Boosts { get => boosts; }
    public Transform MiddleOfBody => middleOfBody;

    [SerializeField] private bool spawnHitVFX = true;

    public Boost GetBoostFromBoostType(BoostType givenType)
    {
        foreach (var item in boosts)
        {
            if (item.BoostType == givenType)
            {
                return item;
            }
        }
        return null;
    }

    public virtual void Awake()
    {
        SetUp();
    }

    protected virtual void SetUp()
    {
        Effectable?.CacheOwner(this);
        Damageable.CacheOwner(this);
        StatSheet.DecayingHealth.CacheMax(StatSheet.MaxHp);
        if (spawnHitVFX)
        {
            damageable.OnTotalDamageCalcRecieve.AddListener(PlaceHitParticle);
        }
        damageable.OnHealGFX.AddListener(PlaceHealParticle);
    }

    private void PlaceHitParticle(Attack givenAttack, Damageable target)
    {
        if (givenAttack.DamageHandler.GetFinalMult() <= 0 || ReferenceEquals(GameManager.Instance.ObjectPoolsHandler, null))
        {
            return;
        }
        ParticleEvents particle = GameManager.Instance.ObjectPoolsHandler.HitParticle.GetPooledObject();
        particle.transform.position = middleOfBody.position;
        particle.gameObject.SetActive(true);
    }

    private void PlaceHealParticle()
    {
        if (ReferenceEquals(GameManager.Instance?.ObjectPoolsHandler, null))
        {
            return;
        }
        ParticleEvents particle = GameManager.Instance.ObjectPoolsHandler.HealParticle.GetPooledObject();
        particle.transform.position = middleOfBody.position;
        particle.gameObject.SetActive(true);
    }
    //call add force on enemies 
    public virtual void ApplyKnockBack(Vector3 normalizedDir)
    {

    }
}


[System.Serializable]
public class Boost
{
    public float BoostValue;
    public float BoostDuration;
    public BoostType BoostType;

}


public enum BoostType
{
    Speed,
    Power,
    Swag
}