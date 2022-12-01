using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    [SerializeField] GameManager _gameManagerPrefab;
    [SerializeField] PlayerManager _playerManager;
    public PlayerManager GetPlayerManager => _playerManager;
    public void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(_gameManagerPrefab);
        }
        GameManager.Instance.AddGameplayManagerHelper(this);
    }
    public void LoadScene(int sceneNumber)
    {
        GameManager.Instance.SceneManager.LoadNextScene(sceneNumber);
    }
}
