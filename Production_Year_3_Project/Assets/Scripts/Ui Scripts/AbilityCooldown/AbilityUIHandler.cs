using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AbilityUIHandler : MonoBehaviour
{

#if UNITY_EDITOR
    [Header("Test")]
    [SerializeField] Ability shownAbility;
    [Button("Activate Cooldown")]
    public void TestActivateCooldown()
    {
        UseAbility(shownAbility);
    }
    [Button("Set New Ability")]
    public void TestSetNewAbility()
    {
        RecievingNewAbility(shownAbility);
    }
    [Button("Reset Ability Cooldown")]
    public void TestReset()
    {
        ResetAbilityCooldown();
    }

#endif
    [SerializeField] Image _cooldownImage;
    [SerializeField] Image currentImage;
    [SerializeField] Sprite empty;
    Coroutine activeRoutine;
    float _maxAbilityCooldown;

    private void Awake()
    {
        _cooldownImage.fillAmount = 0;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (currentImage == null)
        {
            currentImage = GetComponent<Image>();
        }
    }
#endif
    IEnumerator CoolDown()
    {
        while (_cooldownImage.fillAmount > 0)//cooldown seconds
        {
            _cooldownImage.fillAmount -= Time.deltaTime/ _maxAbilityCooldown;
            yield return new WaitForEndOfFrame();
        }
        ResetAbilityCooldown();
    }
    public void RecievingNewAbility(Ability abilitySO)
    {
        if (ReferenceEquals(abilitySO, null))
        {
            currentImage.sprite = empty;
            return;
        }
        SetValueForCooldown(abilitySO.CoolDown);
        currentImage.sprite = abilitySO.AbilityArtwork;
        ResetAbilityCooldown();
    }
    public void UseAbility(Ability abilitySO)
    {

        if (!ReferenceEquals(activeRoutine, null))
        {
            StopCoroutine(activeRoutine);
        }
        _cooldownImage.fillAmount = 1;
        activeRoutine = StartCoroutine(CoolDown());
    }
    void ResetAbilityCooldown()
    {
        if (!ReferenceEquals(activeRoutine, null))
        {
            StopCoroutine(activeRoutine);
        }
        _cooldownImage.fillAmount = 0;
    }
    void SetValueForCooldown(float value)
    {
        _cooldownImage.fillAmount = 0;
        _maxAbilityCooldown = value;
    }
    public void ResetAbilityImage()
    {
        currentImage.sprite = empty;
    }
}
