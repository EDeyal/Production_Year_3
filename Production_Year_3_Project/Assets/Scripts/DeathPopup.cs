using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPopup : Popup
{
    protected override void SubscribeToUiManager()
    {
        base.SubscribeToUiManager();
        GameManager.Instance.UiManager.CacheDeathPopup(this);
    }

    public void ResetScene()
    {
        GameManager.Instance.SceneManager.ResetActiveScene();
    }
}
