using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    //add new object pools here
    [SerializeField] private AbilityStealParticleObjectPool abiltiyStealParticle;
    [SerializeField] private SwordSlashObjectPooler swordSlashVFX;

    private void Start()
    {
        GameManager.Instance.CacheObjectPoolsHandler(this);
    }
    public AbilityStealParticleObjectPool AbiltiyStealParticle { get => abiltiyStealParticle; }
    public SwordSlashObjectPooler SwordSlashVFX { get => swordSlashVFX; }
}
