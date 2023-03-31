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
            GameManager.Instance.SaveManager.SavePointHandler.SetPlayerSavePoint(point);
        }
    }
}
