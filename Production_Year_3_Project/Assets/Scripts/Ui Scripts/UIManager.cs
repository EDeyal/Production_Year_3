using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHud playerHud;
    private DashPopup dashPopup;
    private DeathPopup deathPopup;
    private AbilityVideoPlayer abilityVideoPlayer;
    private PauseMenu _pauseMenu;
    private InstructionPopUp _instructionPopUp;
    private ProgressionPopUp _progressionPopUp;
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
    public void CacheAbilityVideoPlayer(AbilityVideoPlayer givenAbilityVideoPlayer)
    {
        abilityVideoPlayer = givenAbilityVideoPlayer;
    }
    public void CacheInstructionPopUP(InstructionPopUp givenInstructionPopUp)
    {
        _instructionPopUp = givenInstructionPopUp;
    }
    public void CacheProgressionPopUp(ProgressionPopUp givenProgressionPopUp)
    {
        _progressionPopUp = givenProgressionPopUp;
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
            GameManager.Instance.InputManager.SetCurserVisability(true);
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
    public InstructionPopUp InstructionPopUp => _instructionPopUp;


    public AbilityVideoPlayer AbilityVideoPlayer { get => abilityVideoPlayer; }

    public ProgressionPopUp ProgressionPopUp => _progressionPopUp;

}
