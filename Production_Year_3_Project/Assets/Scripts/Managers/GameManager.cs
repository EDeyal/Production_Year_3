using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Fields
    [SerializeField] SceneManager _sceneManager;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] SettingsManager _settingsManager;
    [SerializeField] SaveManager _saveManager;
    #endregion
    #region Properties
    public SceneManager SceneManager => _sceneManager;
    public PlayerManager PlayerManager => _playerManager;
    public SettingsManager SettingsManager => _settingsManager; 
    public SaveManager SaveManager => _saveManager;
    #endregion
}
