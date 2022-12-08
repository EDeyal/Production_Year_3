using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public virtual void Awake()
    {
        if (GameManager.Instance.PlayerManager == null)
            throw new System.Exception("Player Reference is missing from Game Manager");
        else
            _player = GameManager.Instance.PlayerManager;
    }
    protected PlayerManager _player;
    public PlayerManager PlayerManager { get => _player; set => _player = value; }
    //BaseEnemy is where all of the logic that all enemies hold stands
    public abstract bool IsInRange(int distance);
    private void OnDestroy()
    {
        PlayerManager = null;
    }

}
