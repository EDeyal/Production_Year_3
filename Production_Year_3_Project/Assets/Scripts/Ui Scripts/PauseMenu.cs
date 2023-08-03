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
    }

    [ContextMenu("MainMenu")]
    public void MoveToMainMenu()
    {
        GameManager.Instance.SceneManager.LoadSceneIndex(0);
        GameManager.Instance.PauseGameTimeScale(false);
    }
    public void Continue()
    {
        GameManager.Instance.InputManager.SetCurserVisability(false);
        GameManager.Instance.UiManager.EscapePressed(false);
    }
}
