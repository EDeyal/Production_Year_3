using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private BaseHealthBar healthBar;
    [SerializeField] private BaseHealthBar decayingHealthBar;
    [SerializeField] private AbilityImageTest abilityIcon;
    [SerializeField] private Image blackBG;
    [SerializeField] private float fadeIntoBlackMod = 1;
    [SerializeField] private float fadeFromBlackMod = 1;

    private void Start()
    {
        GameManager.Instance.UiManager.CachePlayerHud(this);
        GameManager.Instance.PlayerManager.SubscirbeUI();
    }

    public IEnumerator FadeFromBlack()
    {
        float counter = 0f;
        Color endColor = new Color(blackBG.color.r, blackBG.color.g, blackBG.color.b, 0);
        while (counter < 1)
        {
            blackBG.color = Color.Lerp(blackBG.color, endColor, counter);
            counter += Time.deltaTime * fadeFromBlackMod;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeToBlack()
    {
        float counter = 0f;
        Color endColor = new Color(blackBG.color.r, blackBG.color.g, blackBG.color.b, 1);
        while (counter < 1)
        {
            blackBG.color = Color.Lerp(blackBG.color, endColor, counter);
            counter += Time.deltaTime * fadeIntoBlackMod;
            yield return new WaitForEndOfFrame();
        }
    }
    public BaseHealthBar HealthBar { get => healthBar; }
    public BaseHealthBar DecayingHealthBar { get => decayingHealthBar; }
    public AbilityImageTest AbilityIcon { get => abilityIcon; }
}
