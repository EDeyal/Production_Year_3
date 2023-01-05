using UnityEngine;

[System.Serializable]
public class BoundHandler
{
    [SerializeField] Bounds _bound;
    [SerializeField] GameObject _holder;
    [SerializeField] Vector3 _boundCenterOffset;
    [SerializeField] Color _boundColor;
    public Bounds Bound => _bound;
#if UNITY_EDITOR
    public void ValidateBounds()
    {
        if (!_holder)
            return;
        _bound.center = GetBoundOffset();
    }

    public void DrawBounds()
    {
        if (!_holder)
            return;
        Gizmos.color = _boundColor;
        Gizmos.DrawWireCube(GetBoundOffset(), _bound.size);
    }
#endif
    private Vector3 GetBoundOffset()
    {
        var boundOffsetPos = new Vector3(
            _holder.transform.position.x + _boundCenterOffset.x,
            _holder.transform.position.y + _boundCenterOffset.y, 
            _holder.transform.position.z + _boundCenterOffset.z);
        return boundOffsetPos;
    }

}
