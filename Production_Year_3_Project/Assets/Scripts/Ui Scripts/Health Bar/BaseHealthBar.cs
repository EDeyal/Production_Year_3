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
    [SerializeField] float maxHPTest;
    [SerializeField] float testingNumber;
    [Button("ReduceHealth")]
    void DecreaseHealth()
    {
        ReduceHp(testingNumber, true);
    }
    [Button("AddHealth")]
    void AddHealth()
    {
        AddHp(testingNumber, true);
    }
    [Button("SetMaxHp")]
    void SetMaxHp()
    {
        SetHealthBar(maxHPTest);
        Debug.Log("Health bar max value: " + healthBar.maxValue);
        Debug.Log("Health bar value: " + healthBar.value);
    }


#endif

    float currentHp;
    [SerializeField] float transitionDuration;
    [SerializeField] AnimationCurve addHealthBarCurve;
    [SerializeField] AnimationCurve reduceHealthBarCurve;
    [SerializeField] Slider healthBar;
    [SerializeField] Color mainColor;



    public void SetHealthBar(float startingHP)
    {
        currentHp = startingHP;
        healthBar.maxValue = startingHP;
        healthBar.value = currentHp;
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
            currentHp += amount;
        if (hasTransition)
        {

            healthBar.DOValue(currentHp, transitionDuration).SetEase(curve);
        }
        else
        {
            healthBar.value = currentHp;
        }
    }




}

