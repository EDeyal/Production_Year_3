using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private HealthBarHandler healthBar;
    [SerializeField] private AbilityUIHandler abilityIcon;
    [SerializeField] private Image blackBG;
    [SerializeField] private Image thanksForPlayingPopup;
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

    public IEnumerator FadeInThanksForPlayingPopup()
    {
        thanksForPlayingPopup.gameObject.SetActive(true);
        float counter = 0f;
        Color endColor = new Color(thanksForPlayingPopup.color.r, thanksForPlayingPopup.color.g, thanksForPlayingPopup.color.b, 1);
        while (counter < 1)
        {
            thanksForPlayingPopup.color = Color.Lerp(thanksForPlayingPopup.color, endColor, counter);
            counter += Time.deltaTime * fadeFromBlackMod;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeInEndScree()
    {
        GameManager.Instance.PlayerManager.LockPlayer();
        yield return StartCoroutine(FadeToBlack());
        StartCoroutine(FadeInThanksForPlayingPopup());
    }
    public HealthBarHandler HealthBar { get => healthBar; }
    public AbilityUIHandler AbilityIcon { get => abilityIcon; }
}
