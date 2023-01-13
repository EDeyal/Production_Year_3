using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private DamageDealingCollider playerMeleeAttackCollider;
    [SerializeField] private PlayerAbilityHandler playerAbilityHandler;
    [SerializeField] private EnemyProximitySensor enemyProximitySensor;

    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public DamageDealingCollider PlayerMeleeAttackCollider { get => playerMeleeAttackCollider; }
    public PlayerAbilityHandler PlayerAbilityHandler { get => playerAbilityHandler; }
    public EnemyProximitySensor EnemyProximitySensor { get => enemyProximitySensor; }

    protected override void SetUp()
    {
        base.SetUp();
        PlayerStatSheet.InitializeStats();
        PlayerMeleeAttackCollider.CacheReferences(PlayerStatSheet.MeleeAttack, DamageDealer);
        playerMeleeAttackAnimationHandler.OnAttackPerformed.AddListener(PlayerController.MidAirGraivtyAttackStop);
        PlayerAbilityHandler.OnEquipAbility.AddListener(CachePlayerOnAbility);
        PlayerController.MovementSpeed = StatSheet.Speed;
        PlayerStatSheet.OnOverrideSpeed.AddListener(PlayerController.SetSpeed);
    }

    private void CachePlayerOnAbility(Ability givenAbility)
    {
        givenAbility.CahceOwner(this);
    }

}
