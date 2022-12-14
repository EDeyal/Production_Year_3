using Unity.VisualScripting;
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
    #endregion
    #region Properties
    public SceneLoaderManager SceneManager => _sceneManager;
    public PlayerManager PlayerManager => _playerManager;
    public SettingsManager SettingsManager => _settingsManager;
    public SaveManager SaveManager => _saveManager;
    public InputManager InputManager => _inputManager;
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
}
