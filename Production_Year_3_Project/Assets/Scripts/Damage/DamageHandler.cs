using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageHandler
{
    [SerializeField] private float baseAmount;
    private List<float> modifiers = new List<float>();

    [SerializeField] private DamageType damageType;
    
    public float BaseAmount { get => baseAmount; set => baseAmount = value; }
    public List<float> Modifiers { get => modifiers; }
    public DamageType DamageType { get => damageType; }

    public void OverrideBaseAmount(float amount)
    {
        baseAmount = amount;
    }
    public void AddModifier(float givenMod)
    {
        modifiers.Add(givenMod);
    }

    public void ClearModifiers()
    {
        modifiers.Clear();
    }

    public float GetFinalMult()
    {
        float final = BaseAmount;
        foreach (var item in modifiers)
        {
            final *= item;
        }
        return final;
    }

    public float GetFinalAdd()
    {
        float final = BaseAmount;
        foreach (var item in modifiers)
        {
            final += item;
        }
        return final;
    }
}


public enum DamageType
{
    Physical,
    Fire,
    Ice
}
