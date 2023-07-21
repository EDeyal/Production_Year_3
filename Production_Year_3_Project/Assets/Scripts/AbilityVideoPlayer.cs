using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class AbilityVideoPlayer : Popup
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip thamulClip;
    [SerializeField] private VideoClip qovaxClip;
    [SerializeField] private VideoClip cemuClip;
    [SerializeField] string thamulAbilityText = "Thamul Ability";
    [SerializeField] string qovaxAbilityText = "Qovax Ability";
    [SerializeField] string cemuAbilityText = "Cemu Ability";

    protected override void SubscribeToUiManager()
    {
        base.SubscribeToUiManager();
        GameManager.Instance.UiManager.CacheAbilityVideoPlayer(this);
    }

    public void SetUpVideoPlayer(BaseEnemy givenEnemy)
    {
        videoPlayer.clip = GetClipFromEnemy(givenEnemy);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Time.timeScale = 0f;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Time.timeScale = 1f;
    }
    private VideoClip GetClipFromEnemy(BaseEnemy givenEnemy)
    {
        if (givenEnemy is ThamulEnemy)
        {
            text.text = thamulAbilityText;
            return thamulClip;
        }
        else if(givenEnemy is QovaxEnemy)
        {
            text.text = qovaxAbilityText;
            return qovaxClip;
        }
        else if (givenEnemy is CemuEnemy)
        {
            text.text = cemuAbilityText;
            return cemuClip;
        }
        return null;
    }

}
