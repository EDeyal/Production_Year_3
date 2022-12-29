using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private PlayerVisuals visuals;
    [SerializeField] private PlayerData data;

    public PlayerVisuals Visuals { get => visuals;}
    public PlayerData Data { get => data;}


    private void Start()
    {
        visuals.PlayerMeleeAttackCollider.CacheReferences(data.PlayerStats.MeleeAttack, data.PlayerDamageDealer);
        data.PlayerStatusEffectable.CacheOwner(this);
        data.PlayerDamageable.CacheOwner(this);
    }
}
