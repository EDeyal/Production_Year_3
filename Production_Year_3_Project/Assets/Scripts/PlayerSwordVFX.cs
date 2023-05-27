using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem quovaxDashParticle;
    [SerializeField] private Color basicColor;
    [SerializeField] private Color chargedColor;
    [SerializeField] private Material bladeMat;
    [SerializeField] private float lerpBackwardsMod = 1;
    public void PlayQuovaxDashParticle()
    {
        return;
        quovaxDashParticle.gameObject.SetActive(true);
        quovaxDashParticle.Clear(true);
        quovaxDashParticle.Play(true);
    }

    public void StopQuovaxDashParticle()
    {
        return;
        quovaxDashParticle.gameObject.SetActive(false);
        quovaxDashParticle.Stop();
    }

    public void ChargeSwordColorLerp(Ability currentAbility)
    {
        return;
        //StartCoroutine(LerpColorCoolDown());
        StartCoroutine(LerpColor(chargedColor, basicColor));
    }
    public void UnChargeSwordColorLerp(Ability currentAbility)
    {
        return;
        StartCoroutine(LerpColor(chargedColor, basicColor));
    }

    private IEnumerator LerpColor(Color startColor, Color endColor)
    {
        float counter = 0;
        while (counter < 1)
        {
            bladeMat.color = Color.Lerp(startColor, endColor, counter);
            counter += Time.deltaTime * lerpBackwardsMod;
            yield return new WaitForEndOfFrame();
        }
        bladeMat.color = endColor;
        yield return StartCoroutine(LerpColorCoolDown());
    }

    private IEnumerator LerpColorCoolDown()
    {
        float counter = 0f;
        while (GameManager.Instance.PlayerManager.PlayerAbilityHandler.CurrentRemainingCooldDown > 0)
        {
            bladeMat.color = Color.Lerp(basicColor, chargedColor, counter);
            counter += Time.deltaTime / GameManager.Instance.PlayerManager.PlayerAbilityHandler.CurrentAbility.CoolDown;
            yield return new WaitForEndOfFrame();
        }
        bladeMat.color = chargedColor;
    }

    private void OnApplicationQuit()
    {
        return;
        //This function makes sure the sword's color doesn't change between sessions, which also helps with GIT not recognizing any changes for each run.
        bladeMat.color = basicColor;
    }
}
