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
            return thamulClip;
        }
        else if(givenEnemy is QovaxEnemy)
        {
            return qovaxClip;
        }
        else if (givenEnemy is CemuEnemy)
        {
            return cemuClip;
        }
        return null;
    }

}
