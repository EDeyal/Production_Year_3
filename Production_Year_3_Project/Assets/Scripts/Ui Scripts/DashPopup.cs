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
        GameManager.Instance.InputManager.LockInputs = true;
        gameObject.SetActive(state);
    }
}
