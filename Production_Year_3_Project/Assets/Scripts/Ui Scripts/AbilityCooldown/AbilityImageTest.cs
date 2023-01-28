using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AbilityImageTest : MonoBehaviour
{

#if UNITY_EDITOR
    [Header("Test")]
    [SerializeField] Sprite abilityOne;
    [SerializeField] Sprite abilityTwo;
    [SerializeField] Sprite abilityThree;
    [SerializeField] Ability shownAbility;
    [SerializeField] Image currentImage;
    [Button("CooldownTest")]
    void Play()
    {
        UseAbility();
    }
    [Button("AssignAbility")]
    void AssignAbility()
    {

        RecievingNewAbility(shownAbility);
    }
#endif
    Coroutine activeRoutine;
    [SerializeField] Slider cooldown;

    private void Awake()
    {
        cooldown.value = 0;
    }
    private void OnValidate()
    {
        if (currentImage == null)
        {
            currentImage = GetComponent<Image>();
        }
    }

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
        SetValueForCooldown(abilitySO.CoolDown);
        // setting sprite
        ResetAbilityCooldown();



    }
    public void UseAbility()
    {
        
        if (!ReferenceEquals(activeRoutine,null))
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
