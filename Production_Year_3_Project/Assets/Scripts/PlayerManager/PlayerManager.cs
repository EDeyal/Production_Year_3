using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private PlayerAbilityHandler playerAbilityHandler;
    [SerializeField] private EnemyProximitySensor enemyProximitySensor;
    [SerializeField] private TestProximitySensor testProximitySensor;
    [SerializeField] private CCFlip playerFlipper;

    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public PlayerAbilityHandler PlayerAbilityHandler { get => playerAbilityHandler; }
    public EnemyProximitySensor EnemyProximitySensor { get => enemyProximitySensor; }
    public TestProximitySensor TestProximitySensor { get => testProximitySensor; }
    public CCFlip PlayerFlipper { get => playerFlipper; }

    protected override void SetUp()
    {
        base.SetUp();
        PlayerStatSheet.InitializeStats();
        playerMeleeAttackAnimationHandler.OnAttackPerformed.AddListener(PlayerController.MidAirGraivtyAttackStop);
        PlayerAbilityHandler.OnEquipAbility.AddListener(CachePlayerOnAbility);
        PlayerController.MovementSpeed = StatSheet.Speed;
        PlayerStatSheet.OnOverrideSpeed.AddListener(PlayerController.SetSpeed);
        playerMeleeAttackAnimationHandler.OnAttackPerformed.AddListener(PlayAttackAnimation);
        PlayerMeleeAttack.OnAttackPerformed.AddListener(playerController.ReleaseJumpHeld);

    }

    private void CachePlayerOnAbility(Ability givenAbility)
    {
        givenAbility.CahceOwner(this);
    }

    private void PlayAttackAnimation()
    {
        playerController.AnimBlender.SetTrigger("Attack");
    }

}
