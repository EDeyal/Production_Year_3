using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] Bounds _bound;
    [SerializeField] protected float _boundOffset;
    [SerializeField] Rigidbody _rb;
    //[SerializeField] protected float _speed;
    public Bounds Bound => _bound;
    public float BoundOffset => _boundOffset;
    public Rigidbody RB => _rb;
    //public float speed => _speed;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_bound.center, _bound.size);
    }

    public virtual bool IsNearBound(float offset)
    {
        if (GeneralFunctions.IsInRange(transform.position,_bound.min,offset)|| GeneralFunctions.IsInRange(transform.position, _bound.max, offset))
        {
            return true;
        }
        return false;
    }
}
