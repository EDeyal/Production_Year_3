using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private PlayerAbilityHandler playerAbilityHandler;
    [SerializeField] private EnemyProximitySensor enemyProximitySensor;
    [SerializeField] private CCFlip playerFlipper;
    [SerializeField] private PlayerSwordVFX swordVFX;
    [SerializeField] private Transform gfx;
    [SerializeField] private Transform feetParticlePoint;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private RoomHandler currentRoom;
    [SerializeField] private RunParticle runParticle;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private EnemyPorximityPointer enemyProximityPointer;
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private Material outlineMat;
    [SerializeField] private SavePointProximity savePointProximityDetector;
    public PlayerStatSheet PlayerStatSheet => StatSheet as PlayerStatSheet;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public PlayerAbilityHandler PlayerAbilityHandler { get => playerAbilityHandler; }
    public EnemyProximitySensor EnemyProximitySensor { get => enemyProximitySensor; }
    public CCFlip PlayerFlipper { get => playerFlipper; }
    public Transform Gfx { get => gfx; }
    public PlayerSwordVFX SwordVFX { get => swordVFX; }
    public PlayerDash PlayerDash { get => playerDash; }
    public RoomHandler CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public EnemyPorximityPointer EnemyProximityPointer { get => enemyProximityPointer;  }
    public SavePointProximity SavePointProximityDetector { get => savePointProximityDetector;}

    private void OnEnable()
    {
        UnLockPlayer();
    }

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
        Damageable.OnDeath.AddListener(LockPlayer);
        Damageable.OnDeath.AddListener(EnableDeathPopup);
        PlayerAbilityHandler.OnCast.AddListener(PlayerDash.ResetDashCoolDoown);
        playerController.GroundCheck.OnGrounded.AddListener(PlaceGroundedParticle);
        playerController.OnJump.AddListener(PlaceJumpParticle);
        playerController.OnStartRunning.AddListener(EnableRunParticle);
        playerController.OnStopRunning.AddListener(DisableRunParticle);
        PlayerController.GroundCheck.OnNotGrounded.AddListener(DisableRunParticle);
        playerDash.OnDash.AddListener(EnableDashParticle);
        playerDash.OnDashEnd.AddListener(DisableDashParticle);
        playerAbilityHandler.OnEquipAbility.AddListener(OnEquipSpecificAbility);
       /* StatSheet.DecayingHealth.onDecayingHealthReduce.AddListener(CheckDecayingHealthAmount);
        StatSheet.DecayingHealth.onDecayingHealthGain.AddListener(CheckDecayingHealthAmount);*/
    }

    private void DisableRunParticle()
    {
        runParticle.DisableParticle();
    }
    private void EnableRunParticle()
    {
        runParticle.RestartParticle();
    }
    private void DisableDashParticle()
    {
        dashParticle.gameObject.SetActive(false);
        dashParticle.Stop();
    }
    private void EnableDashParticle()
    {
        dashParticle.gameObject.SetActive(true);
        dashParticle.Clear();
        dashParticle.Play();
    }
    private void PlaceGroundedParticle()
    {
        ParticleEvents particle = GameManager.Instance.ObjectPoolsHandler.LandObjectPool.GetPooledObject();
        particle.transform.position = feetParticlePoint.position;
        particle.gameObject.SetActive(true);
    }
    private void PlaceJumpParticle()
    {
        /* //ParticleEvents particle = GameManager.Instance.ObjectPoolsHandler.JumpObjectPool.GetPooledObject();
         particle.transform.position = feetParticlePoint.position;
         particle.gameObject.SetActive(true);*/
        jumpParticle.gameObject.SetActive(true);
        jumpParticle.Clear();
        jumpParticle.Play();
    }
    private void EnableDeathPopup()
    {
        GameManager.Instance.UiManager.DeathPopup.TogglePopup(true);
    }
    private void CachePlayerOnAbility(Ability givenAbility)
    {
        givenAbility.CahceOwner(this);
    }

    private void CheckDecayingHealthAmount(float amount)
    {
        if (StatSheet.DecayingHealth.CurrentDecayingHealth > 0)
        {
            EnableOutline();
        }
        else
        {
            DisableOutline();
        }
    }

    private void EnableOutline()
    {
        outlineMat.color = new Color(outlineMat.color.r, outlineMat.color.g, outlineMat.color.b, 70f);
    }
    private void DisableOutline()
    {
        outlineMat.color = new Color(outlineMat.color.r, outlineMat.color.g, outlineMat.color.b, 0f);
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
        GameManager.Instance.UiManager.PlayerHud.DecayingHealthBar.SetHealthBarAtZero(StatSheet.MaxHp);
        Damageable.OnTakeDmgGFX.AddListener(UpdateHpbar);
        Damageable.OnHealGFX.AddListener(UpdateHpbar);
        StatSheet.DecayingHealth.onDecayingHealthReduce.AddListener(UpdateDecayinHpbar);
        StatSheet.DecayingHealth.onDecayingHealthGain.AddListener(UpdateDecayinHpbar);
        PlayerAbilityHandler.OnEquipAbility.AddListener(UpdateAbilityUi);
        playerAbilityHandler.OnCast.AddListener(GameManager.Instance.UiManager.PlayerHud.AbilityIcon.UseAbility);
    }

    private void UpdateHpbar()
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.UpdateBar(Damageable.CurrentHp);
    }
    private void UpdateDecayinHpbar(float amount)
    {
        GameManager.Instance.UiManager.PlayerHud.DecayingHealthBar.UpdateBar(StatSheet.DecayingHealth.CurrentDecayingHealth);
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

    private void OnEquipSpecificAbility(Ability givenAbility)
    {
        if (givenAbility is DashTowardsEnemy)
        {
            enemyProximityPointer.enabled = true;
            enemyProximityPointer.SetActive(true);
        }
        else
        {
            enemyProximityPointer.enabled = false;
            enemyProximityPointer.SetActive(false);
        }
    }

    public void LockPlayer()
    {
        playerController.ResetVelocity();
        PlayerController.CanMove = false;
        PlayerAbilityHandler.CanCast = false;
        PlayerMeleeAttack.CanAttack = false;
    }
    public void UnLockPlayer()
    {
        playerController.ResetVelocity();
        PlayerController.CanMove = true;
        PlayerAbilityHandler.CanCast = true;
        PlayerMeleeAttack.CanAttack = true;
    }

    

  /*  private void LockInputs()
    {
        GameManager.Instance.InputManager.LockInputs = true;
    }*/
}
