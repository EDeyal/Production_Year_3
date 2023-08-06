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

    protected override void Start()
    {
        SubscribeToUiManager();
        GameManager.Instance.InputManager.OnPopUpClosed.AddListener(RespawnPlayer);
        gameObject.SetActive(false);
    }

    public void RespawnPlayer()
    {
        GameManager.Instance.PlayerManager.PlayerController.DisableCC();
        GameManager.Instance.PlayerManager.PlayerController.ZeroGravity();
        GameManager.Instance.PlayerManager.transform.position = GameManager.Instance.SaveManager.SavePointHandler.RespawnToSpawnPoint().position;
        GameManager.Instance.PlayerManager.PlayerRespawn();
        GameManager.Instance.PlayerManager.PlayerController.EnableCC();
        //GameManager.Instance.PlayerManager.gameObject.SetActive(true);
        GameManager.Instance.SoundManager.PlaySound("PlayerRespawn");
        gameObject.SetActive(false);
    }
}
