using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private PlayerAbilityHandler playerAbilityHandler;
    [SerializeField] private EnemyProximitySensor enemyProximitySensor;
    [SerializeField] private TestProximitySensor testProximitySensor;
    [SerializeField] private CCFlip playerFlipper;
    [SerializeField] private PlayerSwordVFX swordVFX;
    [SerializeField] private Transform gfx;

    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public PlayerAbilityHandler PlayerAbilityHandler { get => playerAbilityHandler; }
    public EnemyProximitySensor EnemyProximitySensor { get => enemyProximitySensor; }
    public TestProximitySensor TestProximitySensor { get => testProximitySensor; }
    public CCFlip PlayerFlipper { get => playerFlipper; }
    public Transform Gfx { get => gfx; }
    public PlayerSwordVFX SwordVFX { get => swordVFX; }

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
        DamageDealer.OnKill.AddListener(playerAbilityHandler.OnKillStealSpellEvent);
    }
    private void CachePlayerOnAbility(Ability givenAbility)
    {
        givenAbility.CahceOwner(this);
    }

    private void PlayAttackAnimation()
    {
        playerController.AnimBlender.SetTrigger("Attack");
    }

    public void SubscirbeUI()
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.SetHealthBar(StatSheet.MaxHp);
        Damageable.OnTakeDamage.AddListener(UpdateHpbarTakeDmg);
        Damageable.OnGetHealed.AddListener(UpdateHpbarHeal);
        StatSheet.DecayingHealth.onDecayingHealthReduce.AddListener(UpdateDecayinHpbarTakeDmg);
        StatSheet.DecayingHealth.onDecayingHealthGain.AddListener(UpdateDecayinHpbarHeal);
        PlayerAbilityHandler.OnEquipAbility.AddListener(UpdateAbilityUi);
        playerAbilityHandler.OnCast.AddListener(GameManager.Instance.UiManager.PlayerHud.AbilityIcon.UseAbility);
    }

    private void UpdateHpbarTakeDmg(DamageHandler givenDmg)
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.ReduceHp(givenDmg.GetFinalMult(), true);
    }
    private void UpdateHpbarHeal(DamageHandler givenDmg)
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.AddHp(givenDmg.GetFinalMult(), true);
    }

    private void UpdateDecayinHpbarTakeDmg(float amount)
    {
        GameManager.Instance.UiManager.PlayerHud.DecayingHealthBar.ReduceHp(amount, true);
    }
    private void UpdateDecayinHpbarHeal(float amount)
    {
        GameManager.Instance.UiManager.PlayerHud.DecayingHealthBar.AddHp(amount, true);
    }
    private void UpdateAbilityUi(Ability givenAbility)
    {
        GameManager.Instance.UiManager.PlayerHud.AbilityIcon.RecievingNewAbility(givenAbility);
    }
}
