using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BaseHealthBar : MonoBehaviour
{
#if UNITY_EDITOR
    [Button("ReduceHealth")]
    void DecreaseHealth()
    {
        ReduceHp(15f, true);
    }
    [Button("AddHealth")]
    void AddHealth()
    {
        AddHp(15f, true);
    }
    [Button("SetMaxHp")]
    void SetMaxHp()
    {
        SetHealthBar(75);
        Debug.Log(healthBar.maxValue);
        Debug.Log(healthBar.value);
    } 


#endif

    [SerializeField] float currentHp;
    [SerializeField] float transitionDuration;
    [SerializeField] AnimationCurve addHealthBarCurve;
    [SerializeField] AnimationCurve reduceHealthBarCurve;
    [SerializeField] Slider healthBar;
    [SerializeField] Color mainColor;



    public void SetHealthBar(float hp)
    {
        healthBar.maxValue = hp;
        healthBar.value = hp;
    }

    public void AddMaxHp(float addedAmount, bool replenishHealth = false)
    {

        healthBar.maxValue = addedAmount;
        if (replenishHealth)
        {
            healthBar.value += addedAmount;
        }
    }
    public void AddHp(float amount, bool hasTransition = false)
    {
        ChangeHp(amount, hasTransition, addHealthBarCurve);
    }

    public void ReduceHp(float amount, bool hasTransition = false)
    {
        ChangeHp(-amount, hasTransition, reduceHealthBarCurve);
    }

    protected virtual void ChangeHp(float amount, bool hasTransition, AnimationCurve curve)
    {
        if (hasTransition)
        {
            var endAmount = healthBar.value + amount;
            healthBar.DOValue(endAmount, transitionDuration).SetEase(curve);
        }
        else
        {
            healthBar.value += amount;
        }
    }




}

