using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : Popup
{
    [SerializeField] private Image thanksForPlayingPopup;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI pressC;
    [SerializeField] private float fadeInThankYouPopup = 0.5f;

    protected override void Start()
    {
        SubscribeToUiManager();
        // GameManager.Instance.InputManager.OnPopUpClosed.AddListener(ExitToMainMenu);
        gameObject.SetActive(false);
    }

    protected override void SubscribeToUiManager()
    {
        base.SubscribeToUiManager();
        GameManager.Instance.UiManager.CacheEndGamePopup(this);
    }


    private IEnumerator FadeEndGamePopup(bool fadeIn)
    {
        float counter = 0f;
        float targetAlpha = 0;
        if(fadeIn)
        {
            targetAlpha = 1;
        }

        Color popupEndColor = new Color(thanksForPlayingPopup.color.r, thanksForPlayingPopup.color.g, thanksForPlayingPopup.color.b, targetAlpha);
        Color textsEndColor = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);

        Color popupStartColor = thanksForPlayingPopup.color;
        Color textsStartColor = title.color;

        while (counter < 1)
        {
            thanksForPlayingPopup.color = Color.Lerp(popupStartColor, popupEndColor, counter);
            title.color = Color.Lerp(textsStartColor, textsEndColor, counter);
            pressC.color = Color.Lerp(textsStartColor, textsEndColor, counter);
            counter += Time.deltaTime * fadeInThankYouPopup;
            yield return new WaitForEndOfFrame();
        }
    }

    private void ExitToMainMenu()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutIntoMainMenu());
    }

    private IEnumerator FadeOutIntoMainMenu()
    {
        // Fade out Thanks for playing Popup
        yield return StartCoroutine(FadeEndGamePopup(false));

        // Wait 2 seconds
        yield return new WaitForSeconds(2);

        GameManager.Instance.SoundManager.PlaySound("UISaveGame");
        // Exit to main menu
        GameManager.Instance.UiManager.PauseMenu.MoveToMainMenu();
    }

    public IEnumerator FadeInEndScreen()
    {
        gameObject.SetActive(true);
        GameManager.Instance.PlayerManager.LockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeToBlack());
        yield return StartCoroutine(FadeEndGamePopup(true));
        GameManager.Instance.InputManager.OnPopUpClosed.AddListener(ExitToMainMenu);
    }
}
