using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHud playerHud;
    private DashPopup dashPopup;
    private DeathPopup deathPopup;
    private PauseMenu _pauseMenu;
    public void CachPauseMenu(PauseMenu pauseMenu)
    {
        _pauseMenu = pauseMenu;
    }
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
    public void EscapePressed(bool isPressed)
    {
        Debug.Log("Attempting to pause");
        if (!_pauseMenu)
        {
            Debug.LogWarning("No Pause Menu Found");
            return;
        }
        if (isPressed)
        {
            //open window
            _pauseMenu.PausePanel.SetActive(isPressed);
            GameManager.Instance.PauseGameTimeScale(true);
        }
        else
        {
            GameManager.Instance.PauseGameTimeScale(false);
            _pauseMenu.PausePanel.SetActive(isPressed);
        }
    }
    public PlayerHud PlayerHud { get => playerHud; }
    public DeathPopup DeathPopup { get => deathPopup; }
    public DashPopup DashPopup { get => dashPopup; }
}
