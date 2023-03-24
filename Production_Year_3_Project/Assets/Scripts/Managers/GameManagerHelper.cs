using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    [SerializeField] GameManager _gameManagerPrefab;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] RoomsManager _roomsManager;
    [SerializeField] SavePointHandler _savePointHandler;
    public PlayerManager GetPlayerManager => _playerManager;
    public RoomsManager GetRoomsManager => _roomsManager;
    private void OnValidate()
    {
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }
        if (_roomsManager == null)
        {
            _roomsManager = FindObjectOfType<RoomsManager>();
        }
        if (_savePointHandler == null)
        {
            _savePointHandler = FindObjectOfType<SavePointHandler>();
        }
    }

    public void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(_gameManagerPrefab);
        }
        GameManager.Instance.AddGameplayManagerHelper(this);
        if (_savePointHandler!= null)
        {
            GameManager.Instance.SaveManager.SetSavePointHandler(_savePointHandler);
        }
    }
    public void LoadScene(int sceneNumber)
    {
        GameManager.Instance.SceneManager.LoadSceneIndex(sceneNumber);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR_WIN
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
