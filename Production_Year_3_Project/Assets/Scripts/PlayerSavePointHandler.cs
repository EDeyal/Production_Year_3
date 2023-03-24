using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSavePointHandler : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.InputManager.OnSavePoint.AddListener(SavePoint);
    }

    public void SavePoint()
    {
        SavePoint point = GameManager.Instance.PlayerManager.SavePointProximityDetector.GetClosestLegalTarget();
        if (!ReferenceEquals(point, null))
        {
            if (point.CanSave)
            {
                GameManager.Instance.SaveManager.SavePointHandler.SetPlayerSavePoint(point);
            }
        }
    }
}
