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
    [SerializeField] RoomsManager _roomsManager;
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
    public RoomsManager RoomsManager => _roomsManager;
    #endregion

    public override void Awake()
    {
        base.Awake();
        _inputManager.OnEscape.AddListener(EscapePressed);
        DontDestroyOnLoad(this);
    }
    public void OnDestroy()
    {
        _inputManager.OnEscape.RemoveListener(EscapePressed);
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            InputManager.SetCurserVisability(false);
    }
    public void EscapePressed()
    {
        uiManager.EscapePressed(true);

    }
    public void AddGameplayManagerHelper(GameManagerHelper gameManagerHelper)
    {
        _gameManagerHelper = gameManagerHelper;
        if (_gameManagerHelper.GetPlayerManager != null)
        {
            _playerManager = _gameManagerHelper.GetPlayerManager;
        }
        if (_gameManagerHelper.GetRoomsManager != null)
        {
            _roomsManager = _gameManagerHelper.GetRoomsManager;
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
    public void PauseGameTimeScale(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
}
