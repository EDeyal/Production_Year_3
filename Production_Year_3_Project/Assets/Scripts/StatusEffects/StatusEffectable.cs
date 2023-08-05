using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[RequireComponent(typeof(BaseCharacter))]
public class StatusEffectable : MonoBehaviour
{
    private List<StatusEffect> statusEffects = new List<StatusEffect>();
    private BaseCharacter owner;

    /// <summary>
    /// invoked when recieving a status effect
    /// </summary>
    public UnityEvent<StatusEffect> OnRecieveStatusEffect;
    /// <summary>
    /// invoked when a status effect is canceled
    /// </summary>
    public UnityEvent<StatusEffect> OnStatusEffectRemoved;

    public List<StatusEffect> StatusEffects { get => statusEffects; }
    public BaseCharacter Owner { get => owner; }

    public void CacheOwner(BaseCharacter givenCharacter)
    {
        owner = givenCharacter;
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        foreach (var item in statusEffects)
        {
            if (item.GetType() ==  effect.GetType())
            {
                item.Reset();
                OnRecieveStatusEffect?.Invoke(effect);
                return;
            }
        }
        statusEffects.Add(effect);
        OnRecieveStatusEffect?.Invoke(effect);
        effect.CacheHost(Owner);
        effect.StartEffect();
    }

    public void ApplyStatusEffect(StatusEffect effect, StatusEffector effector)
    {
        foreach (var item in statusEffects)
        {
            if (item.GetType() == effect.GetType())
            {
                item.Reset();
                OnRecieveStatusEffect?.Invoke(effect);
                effector.OnApplyStatusEffect?.Invoke(effect);
                return;
            }
        }

        statusEffects.Add(effect);
        OnRecieveStatusEffect?.Invoke(effect);
        effector.OnApplyStatusEffect?.Invoke(effect);
        effect.CacheHost(Owner);
        effect.StartEffect();
    }


    public void RemoveStatusEffect(StatusEffect effect)
    {
        foreach (var item in statusEffects)
        {
            if (item.GetType() == effect.GetType())
            {
                item.Remove();
                OnStatusEffectRemoved.Invoke(item);
                statusEffects.Remove(item);
                return;
            }
        }
    }
}
