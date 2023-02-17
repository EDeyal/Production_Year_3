using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPopup : Popup
{


    protected override void SubscribeToUiManager()
    {
        base.SubscribeToUiManager();
        GameManager.Instance.UiManager.CacheDashPopup(this);
    }
}
