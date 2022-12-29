using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour, ICheckValidation
{
    protected const float ZERO = 0;
    protected const float ONE = 1;
    #region Fields
    [SerializeField] BoundHandler _boundHandler;
    [SerializeField] Rigidbody _rb;
    [SerializeField] SensorHandler _sensorHandler;
    [SerializeField] BaseAction<DistanceData> _noticePlayerDistance;
    [SerializeField] BaseAction<DistanceData> _chasePlayerDistance;
    [SerializeField] AnimatorHandler _animatorHandler;
    [SerializeField] EnemyStatSheet _enemyStatSheet;
    #endregion

    #region Properties
    public Rigidbody RB => _rb;
    public SensorHandler SensorHandler => _sensorHandler;
    public BaseAction<DistanceData> NoticePlayerDistance => _noticePlayerDistance;
    public BaseAction<DistanceData> chasePlayerDistance => _chasePlayerDistance;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public EnemyStatSheet EnemyStatSheet => _enemyStatSheet;
    public BoundHandler BoundHandler => _boundHandler;
    #endregion
    private void OnValidate()
    {
        _boundHandler.ValidateBounds();
    }
    public virtual void CheckValidation()
    {
        if (!_sensorHandler)
            throw new System.Exception("BaseEnemy has no SensorHandler");
    }
    private void OnDrawGizmos()
    {
        BoundHandler.DrawBounds();
    }
}
