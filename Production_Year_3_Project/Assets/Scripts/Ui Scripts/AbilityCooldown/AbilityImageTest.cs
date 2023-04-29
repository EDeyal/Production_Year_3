using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityImageTest : MonoBehaviour
{

#if UNITY_EDITOR
    [Header("Test")]
    [SerializeField] Sprite abilityOne;
    [SerializeField] Sprite abilityTwo;
    [SerializeField] Sprite abilityThree;
    [SerializeField] Ability shownAbility;
#endif
    [SerializeField] Image currentImage;
    [SerializeField] Sprite empty;
    Coroutine activeRoutine;
    [SerializeField] Slider cooldown;

    private void Awake()
    {
        cooldown.value = 0;
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
        while (cooldown.value > 0)//cooldown seconds
        {
            cooldown.value -= Time.deltaTime;
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
        cooldown.value = cooldown.maxValue;
        activeRoutine = StartCoroutine(CoolDown());

    }
    void ResetAbilityCooldown()
    {
        if (!ReferenceEquals(activeRoutine, null))
        {
            StopCoroutine(activeRoutine);
        }
        cooldown.value = 0;
    }
    void SetValueForCooldown(float value)
    {
        cooldown.maxValue = value;
    }
}
