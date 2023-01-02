using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private DamageDealingCollider playerMeleeAttackCollider;

    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public DamageDealingCollider PlayerMeleeAttackCollider { get => playerMeleeAttackCollider; }

    private void Start()
    {
        PlayerMeleeAttackCollider.CacheReferences(PlayerStatSheet.MeleeAttack, DamageDealer);
        Effectable.CacheOwner(this);
        Damageable.CacheOwner(this);
    }
}
