public abstract class StatusEffect
{
    protected StatusEffector effector;
    public StatusEffect(StatusEffector givenEffector = null)
    {
        effector = givenEffector;
    }

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
