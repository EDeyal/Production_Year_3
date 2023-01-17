using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DecayingHealth 
{
    private float currentDecayingHealth;
    private Coroutine activeDecayingRoutine;
    public UnityEvent onDecayingHealthChange;
    private float currentPossibleMax;

    public float CurrentDecayingHealth { get => currentDecayingHealth; set => currentDecayingHealth = value; }

    public void CacheMax(float givenMax)
    {
        currentPossibleMax = givenMax;
    }

    public void AddDecayingHealth(float amount)
    {
        currentDecayingHealth += amount;
        onDecayingHealthChange?.Invoke();
        ClampHealth();
        if (!ReferenceEquals(activeDecayingRoutine, null))
        {
            GameManager.Instance.StopCoroutine(activeDecayingRoutine);
        }
        activeDecayingRoutine =  GameManager.Instance.StartCoroutine(DecayHealth());
    }

    public void ClampHealth()
    {
        currentDecayingHealth = Mathf.Clamp(currentDecayingHealth, 0, currentPossibleMax);
    }

    private IEnumerator DecayHealth()
    {
        while (currentDecayingHealth > 0)
        {
            currentDecayingHealth -= 10;
            onDecayingHealthChange?.Invoke();
            ClampHealth();
            yield return new WaitForSecondsRealtime(1f);
        }
        
        currentDecayingHealth = 0;
    }
}
