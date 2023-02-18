using UnityEngine;
public abstract class BaseStateHandler :MonoBehaviour, ICheckValidation
{
    [SerializeField] private BaseEnemy _refEnemy;
    [SerializeField] private BaseState _currentState;
    [SerializeField] private BaseState _idleState;
    [SerializeField] private BaseState _combatState;
    [SerializeField] private BaseState _deathState;
    [SerializeField] private BaseState _knockBackState;

    protected PlayerManager _player;

    public BaseState CurrentState { get => _currentState; set => _currentState = value; }
    public BaseState IdleState => _idleState;
    public BaseState CombatState => _combatState;
    public BaseState DeathState => _deathState; 
    public BaseState KnockBackState => _knockBackState;
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
        if (_deathState == null)
            throw new System.Exception("Death state is Null");
        if (_knockBackState == null)
            throw new System.Exception("Knockback state is Null");
    }
    protected virtual void OnEnable()
    {
        _currentState = IdleState;
    }
}
