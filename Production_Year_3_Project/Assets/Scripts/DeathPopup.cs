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

    public void RespawnPlayer()
    {
        GameManager.Instance.PlayerManager.gameObject.SetActive(false);
        GameManager.Instance.PlayerManager.transform.position = GameManager.Instance.SaveManager.SavePointHandler.RespawnToSpawnPoint().position;
        GameManager.Instance.PlayerManager.PlayerRespawn();
        GameManager.Instance.PlayerManager.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
}
