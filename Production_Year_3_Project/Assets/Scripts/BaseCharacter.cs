using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Damageable damageable;
    [SerializeField] private StatusEffectable effectable;
    [SerializeField] private StatusEffector effector;
    [SerializeField] private StatSheet statSheet;
    [SerializeField] private List<Boost> boosts = new List<Boost>();
    public DamageDealer DamageDealer { get => damageDealer; }
    public Damageable Damageable { get => damageable; }
    public StatusEffectable Effectable { get => effectable; }
    public StatusEffector Effector { get => effector; }
    public StatSheet StatSheet { get => statSheet; }
    public List<Boost> Boosts { get => boosts; }

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
        Effectable.CacheOwner(this);
        Damageable.CacheOwner(this);
        StatSheet.DecayingHealth.CacheMax(StatSheet.MaxHp);
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