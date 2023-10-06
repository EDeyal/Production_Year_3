using UnityEngine;

public class MainMenuHandler : MonoBehaviour, ICheckValidation
{
    [SerializeField] GameObject _mainMenuScreen;
    [SerializeField] GameObject _creditsScreen;
    [SerializeField] int _gameSceneIndex = 1;

    public void CheckValidation()
    {
        if (!_mainMenuScreen)
            throw new System.Exception("MainMenuHandler has no mainMenuScreen");
        if (!_creditsScreen)
            throw new System.Exception("MainMenuHandler has no Credits Screen");
    }

    private void Awake()
    {
        _mainMenuScreen.gameObject.SetActive(true);
        _creditsScreen.gameObject.SetActive(false);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //SetCurserVisable(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            GameManager.Instance.InputManager.SetCurserVisability(true);
        }
    }
    public void StartGame()
    {
        GameManager.Instance.InputManager.SetCurserVisability(false);
        GameManager.Instance.SceneManager.LoadSceneIndex(_gameSceneIndex);
    }
}
