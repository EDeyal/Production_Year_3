public interface IRespawnable
{
    /// <summary>
    /// When calling this function this asset will respawn on reset of save point
    /// </summary>
    public virtual void RespawnSubscribe()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Add(this);
    }
    /// <summary>
    /// When calling this function this asset will not respawn any more
    /// </summary>
    public virtual void RespawnUnsubscribe()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Remove(this);
    }
    public void Respawn();
}
