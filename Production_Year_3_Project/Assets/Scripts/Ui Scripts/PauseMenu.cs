using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseHolder;
    public GameObject PausePanel => _pauseHolder;
    private void Awake()
    {
        _pauseHolder.SetActive(false);
    }
    private void Start()
    {
        GameManager.Instance.UiManager.CachPauseMenu(this);
        GameManager.Instance.InputManager.OnPopUpClosed.AddListener(Continue);
    }

    [ContextMenu("MainMenu")]
    public void MoveToMainMenu()
    {
        GameManager.Instance.SceneManager.LoadSceneIndex(0);
        GameManager.Instance.PauseGameTimeScale(false);
        GameManager.Instance.InputManager.SetCurserVisability(true);
    }
    public void Continue()
    {
        GameManager.Instance.InputManager.SetCurserVisability(false);
        GameManager.Instance.UiManager.EscapePressed(false);
    }
}
