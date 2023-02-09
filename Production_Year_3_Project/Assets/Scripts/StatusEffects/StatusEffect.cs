using UnityEngine.Events;

public abstract class StatusEffect
{
    protected BaseCharacter host;

    public virtual void StartEffect()
    {
        Subscribe();
    }

    public virtual void CacheHost(BaseCharacter character)
    {
        host = character;
    }


    public virtual void Remove()
    {
        UnSubscribe();
    }

    public abstract void Reset();

    protected abstract void Subscribe();
    protected abstract void UnSubscribe();

}
