public abstract class StatusEffect
{
    protected BaseCharacter host;

    public virtual void StartEffect()
    {
        Subscribe();
    }

    public virtual void Remove()
    {
        UnSubscribe();
    }

    public abstract void Reset();

    protected abstract void Subscribe();
    protected abstract void UnSubscribe();



}
