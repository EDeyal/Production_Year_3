using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Fields
    [SerializeField] SceneLoaderManager _sceneManager;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] SettingsManager _settingsManager;
    [SerializeField] SaveManager _saveManager;
    [SerializeField] GameManagerHelper _gameManagerHelper;
    [SerializeField] InputManager _inputManager;
    [SerializeField] ObjectPoolHandler objectPoolsHandler;
    [SerializeField] UIManager uiManager;
    [SerializeField] CameraMovement cam;
    #endregion
    #region Properties
    public SceneLoaderManager SceneManager => _sceneManager;
    public PlayerManager PlayerManager => _playerManager;
    public SettingsManager SettingsManager => _settingsManager;
    public SaveManager SaveManager => _saveManager;
    public InputManager InputManager => _inputManager;

    public ObjectPoolHandler ObjectPoolsHandler { get => objectPoolsHandler; }
    public UIManager UiManager { get => uiManager; }
    public CameraMovement Cam { get => cam;}
    #endregion

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void AddGameplayManagerHelper(GameManagerHelper gameManagerHelper)
    {
        _gameManagerHelper = gameManagerHelper;
        if (_gameManagerHelper.GetPlayerManager != null)
        {
            _playerManager = _gameManagerHelper.GetPlayerManager;
        }
    }
    public void CacheObjectPoolsHandler(ObjectPoolHandler givenHandler)
    {
        objectPoolsHandler = givenHandler;
    }
    public void CacheCam(CameraMovement givenCam)
    {
        cam = givenCam;
    }
}
