using UnityEngine;

public abstract class BaseAction<T> : ScriptableObject, ICheckValidation
{
    public virtual void CheckValidation() { }

    public abstract bool InitAction(T data);

    public virtual void DrawGizmos(Vector3 Pos) { }
}
