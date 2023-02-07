using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHud playerHud;

    public void CachePlayerHud(PlayerHud givenHud)
    {
        playerHud = givenHud;
    }

    public void CacheDashPopup(DashPopup givenDashPopup)
    {
        DashPopup = givenDashPopup;
    }

    public PlayerHud PlayerHud { get => playerHud; }

    public DashPopup DashPopup;
}
