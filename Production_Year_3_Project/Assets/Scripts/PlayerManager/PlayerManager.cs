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
    [SerializeField] private PlayerDash playerDash;
    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public PlayerAbilityHandler PlayerAbilityHandler { get => playerAbilityHandler; }
    public EnemyProximitySensor EnemyProximitySensor { get => enemyProximitySensor; }
    public TestProximitySensor TestProximitySensor { get => testProximitySensor; }
    public CCFlip PlayerFlipper { get => playerFlipper; }
    public Transform Gfx { get => gfx; }
    public PlayerSwordVFX SwordVFX { get => swordVFX; }
    public PlayerDash PlayerDash { get => playerDash; }

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
        PlayerController.CacheKnockBackDuration(PlayerStatSheet.KnockBackDuration);
        PlayerAbilityHandler.OnCast.AddListener(SwordVFX.ChargeSwordColorLerp);
        Damageable.OnTotalDamageCalcRecieve.AddListener(PlayHitAnimation);
        Damageable.OnDeath.AddListener(PlayDeathAnimation);
        Damageable.OnDeath.AddListener(PlayerController.ResetVelocity);
        Damageable.OnDeath.AddListener(PlayerController.ResetGravity);
    }
    private void CachePlayerOnAbility(Ability givenAbility)
    {
        givenAbility.CahceOwner(this);
    }

    private void PlayAttackAnimation()
    {
        playerController.AnimBlender.SetTrigger("Attack");
    }
    private void PlayHitAnimation(Attack givenAttack, Damageable target)
    {
        if (givenAttack.DamageHandler.GetFinalMult() > 0)
        {
            playerController.AnimBlender.SetTrigger("GetHit");
        }
    }
    private void PlayDeathAnimation()
    {
        playerController.AnimBlender.SetTrigger("Die");
        //lock inputs 
        playerController.CanMove = false;
        PlayerAbilityHandler.CanCast = false;
        PlayerMeleeAttack.CanAttack = false;
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

    private void UpdateHpbarTakeDmg(Attack givenAttack, Damageable target)
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.ReduceHp(givenAttack.DamageHandler.GetFinalMult(), true);
    }
    private void UpdateHpbarHeal(DamageHandler givenDamage, Damageable target)
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.AddHp(givenDamage.GetFinalMult(), true);
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
    public override void ApplyKnockBack(Vector3 normalizedDir)
    {
        //only apply if damage taken > 0
        Debug.Log("Pushing player");
        playerController.AddForce(normalizedDir * PlayerStatSheet.TakeDamageKnockBackForce);
    }
}
