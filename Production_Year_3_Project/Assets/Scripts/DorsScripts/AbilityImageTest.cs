using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityImageTest : MonoBehaviour
{
    [SerializeField] Sprite abilityOne;
    [SerializeField] Sprite abilityTwo;
    [SerializeField] Sprite abilityThree;
    [SerializeField] Image currentImage;
    [SerializeField] Slider cooldown;
    [SerializeField] float lerpDuration;

    private void Start()
    {
        StartCoroutine(CoolDown());
    }
    private void Awake()
    {

        currentImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            currentImage.sprite = abilityOne;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentImage.sprite = abilityTwo;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            currentImage.sprite = abilityThree;
        }
    }
    

    IEnumerator CoolDown()
    {    
        while (cooldown.value > 0)//cooldown seconds
        {
            float counter = 0;
            while (counter < 1)
            {
                yield return new WaitForEndOfFrame();
                counter += Time.deltaTime;
                cooldown.value -= Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        cooldown.value = 0;
    }

    void SetValueForCooldown(float value)
    {
        cooldown.maxValue = value;
    }
}
