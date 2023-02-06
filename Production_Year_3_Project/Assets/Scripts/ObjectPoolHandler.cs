using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    //add new object pools here
    [SerializeField] private ParticleObjectPooler abiltiyStealParticle;
    [SerializeField] private ParticleObjectPooler hitParticle;
    [SerializeField] private SwordSlashObjectPooler swordSlashVFX;

    private void Start()
    {
        GameManager.Instance.CacheObjectPoolsHandler(this);
    }
    public ParticleObjectPooler AbiltiyStealParticle { get => abiltiyStealParticle; }
    public SwordSlashObjectPooler SwordSlashVFX { get => swordSlashVFX; }
    public ParticleObjectPooler HitParticle { get => hitParticle; }
}
