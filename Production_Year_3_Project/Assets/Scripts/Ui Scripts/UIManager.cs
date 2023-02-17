using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHud playerHud;
    private DashPopup dashPopup;
    private DeathPopup deathPopup;

    public void CachePlayerHud(PlayerHud givenHud)
    {
        playerHud = givenHud;
    }

    public void CacheDashPopup(DashPopup givenDashPopup)
    {
        dashPopup = givenDashPopup;
    }
    public void CacheDeathPopup(DeathPopup givenDeathPopup)
    {
        deathPopup = givenDeathPopup;
    }
    public PlayerHud PlayerHud { get => playerHud; }
    public DeathPopup DeathPopup { get => deathPopup; }
    public DashPopup DashPopup { get => dashPopup; }
}
