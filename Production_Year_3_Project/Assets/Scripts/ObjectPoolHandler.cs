using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    //add new object pools here
    [SerializeField] private ParticleObjectPooler abiltiyStealParticle;
    [SerializeField] private ParticleObjectPooler hitParticle;
    [SerializeField] private ParticleObjectPooler healParticle;
    [SerializeField] private ParticleObjectPooler closestEnemyParticle;
    [SerializeField] private ParticleObjectPooler landObjectPool;
    [SerializeField] private SwordSlashObjectPooler swordSlashVFX;
    [SerializeField] private ProjectileObjectPool playerProjectilePool;
    [SerializeField] private ProjectileObjectPool thamulProjectilePool;
    private void Start()
    {
        GameManager.Instance.CacheObjectPoolsHandler(this);
        DontDestroyOnLoad(this);
    }
    public ParticleObjectPooler AbiltiyStealParticle { get => abiltiyStealParticle; }
    public SwordSlashObjectPooler SwordSlashVFX { get => swordSlashVFX; }
    public ParticleObjectPooler HitParticle { get => hitParticle; }
    public ParticleObjectPooler HealParticle { get => healParticle; }
    public ParticleObjectPooler ClosestEnemyParticle { get => closestEnemyParticle; }
    public ParticleObjectPooler LandObjectPool { get => landObjectPool; }
    public ProjectileObjectPool PlayerProjectileObjectPool { get => playerProjectilePool; }
    public ProjectileObjectPool ThamulProjectilePool { get => thamulProjectilePool; }
}
