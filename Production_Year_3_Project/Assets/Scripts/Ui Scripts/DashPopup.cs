using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPopup : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.UiManager.CacheDashPopup(this);
        gameObject.SetActive(false);
    }
    public void ToggleDashPopup(bool state)
    {

        Debug.Log("Toggle");
        GameManager.Instance.PlayerManager.enabled = !state;
        GameManager.Instance.InputManager.LockInputs = state;
        gameObject.SetActive(state);
    }
}
