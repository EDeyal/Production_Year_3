using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayingHealth 
{
    private float currentDecayingHealth;
    private Coroutine activeDecayingRoutine;

    public float CurrentDecayingHealth { get => currentDecayingHealth; set => currentDecayingHealth = value; }

    public void AddDecayingHealth(float amount)
    {
        currentDecayingHealth += amount;
        if (!ReferenceEquals(activeDecayingRoutine, null))
        {
            GameManager.Instance.StopCoroutine(activeDecayingRoutine);
        }
        activeDecayingRoutine =  GameManager.Instance.StartCoroutine(DecayHealth());
    }


    private IEnumerator DecayHealth()
    {
        while (currentDecayingHealth > 0)
        {
            currentDecayingHealth -= 10;
            yield return new WaitForSecondsRealtime(1f);
        }
        currentDecayingHealth = 0;
    }
}
