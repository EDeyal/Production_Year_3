using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusEffectable : MonoBehaviour
{
    private List<StatusEffect> statusEffects = new List<StatusEffect>();

    /// <summary>
    /// invoked when recieving a status effect
    /// </summary>
    public UnityEvent<StatusEffect> OnRecieveStatusEffect;
    /// <summary>
    /// invoked when a status effect is canceled
    /// </summary>
    public UnityEvent<StatusEffect> OnStatusEffectRemoved;


    public List<StatusEffect> StatusEffects { get => statusEffects; }


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
        effect.StartEffect();
    }


    public void RemoveStatusEffect(StatusEffect effect)
    {
        foreach (var item in statusEffects)
        {
            if (item.GetType() == effect.GetType())
            {
                statusEffects.Remove(item);
                item.Remove();
                return;
            }
        }
    }

}
