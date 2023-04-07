using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private void Start()
    {
        SubscribeToUiManager();
        gameObject.SetActive(false);
    }

    protected virtual void SubscribeToUiManager()
    {
        //debug log sus
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
        gameObject.SetActive(state);
    }

}
