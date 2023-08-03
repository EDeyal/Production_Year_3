using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    protected virtual void Start()
    {
        SubscribeToUiManager();
        GameManager.Instance.InputManager.OnPopUpClosed.AddListener(PopupOff);
        gameObject.SetActive(false);
    }
    
    protected void PopupOff()
    {
        gameObject.SetActive(false);
        UnlockPlayer();
    }

    protected virtual void SubscribeToUiManager()
    {
    }
    protected virtual void OnEnable()
    {
        Cursor.visible = true;
    }
    protected virtual void OnDisable()
    {
        Cursor.visible = false;
    }
    public void TogglePopup(bool state)
    {
        //GameManager.Instance.PlayerManager.enabled = !state;
        if (state)
        {
            GameManager.Instance.PlayerManager.LockPlayer();
        }
        else
        {
            GameManager.Instance.PlayerManager.UnLockPlayer();
        }
        gameObject.SetActive(state);
    }
    public void UnlockPlayer()
    {
        GameManager.Instance.PlayerManager.UnLockPlayer();
    }

}
