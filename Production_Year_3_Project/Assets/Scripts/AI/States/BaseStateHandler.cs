using UnityEngine;
public abstract class BaseStateHandler :MonoBehaviour, ICheckValidation
{
    [SerializeField] private int _noticePlayerDistance;
    [SerializeField] private BaseEnemy _refEnemy;
    [SerializeField] private BaseState _currentState;
    [SerializeField] private BaseState _idleState;
    [SerializeField] private BaseState _combatState;
    protected PlayerManager _player;

    public BaseState CurrentState { get => _currentState; set => _currentState = value; }
    public BaseState IdleState { get => _idleState; }
    public BaseState CombatState { get => _combatState; }
    public int NoticePlayerDistance => _noticePlayerDistance;
    public PlayerManager PlayerManager => _player;
    public BaseEnemy RefEnemy => _refEnemy;
    public virtual void Start()
    {
        if (GameManager.Instance.PlayerManager == null)
            throw new System.Exception("Player Reference is missing from Game Manager");
        else
            _player = GameManager.Instance.PlayerManager;
    }
    //BaseEnemy is where all of the logic that all enemies hold stands
    private void OnDestroy()
    {
        _player = null;
    }
    public virtual void CheckValidation()
    {
        if (_currentState == null)
            throw new System.Exception("Current state is Null");
        if(_idleState == null)
            throw new System.Exception("Idle state is Null");
        if (_combatState == null)
            throw new System.Exception("Combat state is Null");
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(RefEnemy.transform.position, NoticePlayerDistance);
    }
}
