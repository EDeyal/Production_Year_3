using System.Collections;
using UnityEngine;

public class PlayerManager : BaseCharacter
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private PlayerAbilityHandler playerAbilityHandler;
    [SerializeField] private EnemyProximitySensor enemyProximitySensor;
    [SerializeField] private DamageAbleTerrainProximitySensor damageableTerrainProximitySensor;
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
    [SerializeField] private ParticleSystem progressionParticle;
    [SerializeField] private Material outlineMat;
    [SerializeField] private SavePointProximity savePointProximityDetector;
    [SerializeField] private PlayerSavePointHandler playerSaveHandler;
    [SerializeField] private PlayerSoundPlayer soundPlayer;
    private bool attackState;
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
    public EnemyPorximityPointer EnemyProximityPointer { get => enemyProximityPointer; }
    public SavePointProximity SavePointProximityDetector { get => savePointProximityDetector; }
    public DamageAbleTerrainProximitySensor DamageableTerrainProximitySensor { get => damageableTerrainProximitySensor; }
    public PlayerSoundPlayer SoundPlayer { get => soundPlayer; }

    private void OnEnable()
    {
        UnLockPlayer();
    }

    protected override void SetUp()
    {
        base.SetUp();
        PlayerStatSheet.InitializeStats();
        playerMeleeAttackAnimationHandler.OnAttackPerformedVisual.AddListener(PlayerController.MidAirGraivtyAttackStop);
        PlayerAbilityHandler.OnEquipAbility.AddListener(CachePlayerOnAbility);
        PlayerController.MovementSpeed = StatSheet.Speed;
        PlayerStatSheet.OnOverrideSpeed.AddListener(PlayerController.SetSpeed);
        playerMeleeAttackAnimationHandler.OnAttackPerformedVisual.AddListener(PlayAttackAnimation);
        PlayerMeleeAttack.OnAttackPerformedVisual.AddListener(playerController.ReleaseJumpHeld);
        DamageDealer.OnKill.AddListener(playerAbilityHandler.OnKillStealSpellEvent);
        PlayerController.CacheKnockBackDuration(PlayerStatSheet.KnockBackDuration);
        PlayerAbilityHandler.OnCast.AddListener(SwordVFX.ChargeSwordColorLerp);
        Damageable.OnTotalDamageCalcRecieve.AddListener(PlayHitAnimation);
        Damageable.OnDeath.AddListener(PlayDeathAnimation);
        Damageable.OnDeath.AddListener(PlayerController.ResetVelocity);
        Damageable.OnDeath.AddListener(PlayerController.ResetGravity);
        Damageable.OnDeath.AddListener(LockPlayer);
        Damageable.OnDeath.AddListener(EnableDeathPopup);
        Damageable.OnDeath.AddListener(EquipEmptyAbility);
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
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        if (!ReferenceEquals(playerSaveHandler, null))
        {
            playerSaveHandler.SetStartingSavePoint();
        }
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
    public void PlayProgressionParticle(Color particleColor)
    {
        progressionParticle.Clear();
        progressionParticle.startColor = particleColor;
        progressionParticle.Play();
    }
    private void EnableDeathPopup()
    {
        GameManager.Instance.UiManager.DeathPopup.TogglePopup(true);
    }
    private void CachePlayerOnAbility(Ability givenAbility)
    {
        /* if (ReferenceEquals(givenAbility, null))
         {
             return;
         }*/
        givenAbility?.CahceOwner(this);
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
        if (attackState && PlayerController.GroundCheck.IsGrounded())
        {
            playerController.AnimBlender.Anim.Play("SlashUp");

        }
        else
        {
            playerController.AnimBlender.Anim.Play("SlashDown");

        }
        attackState = !attackState;
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
        playerController.AnimBlender.SetBool("Die", true);
        playerController.CanMove = false;
        PlayerAbilityHandler.CanCast = false;
        PlayerMeleeAttack.CanAttack = false;
    }
    public void SubscirbeUI()
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.InitHealthBars(StatSheet.MaxHp);
        Damageable.OnGainMaxHP.AddListener(AddMaxHPUI);
        Damageable.OnTakeDmgGFX.AddListener(UpdateHpbar);
        Damageable.OnHealGFX.AddListener(UpdateHpbar);
        PlayerAbilityHandler.OnEquipAbility.AddListener(UpdateAbilityUi);
        playerAbilityHandler.OnCast.AddListener(GameManager.Instance.UiManager.PlayerHud.AbilityIcon.UseAbility);
    }

    private void UpdateHpbar()
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.UpdateHP(Damageable.CurrentHp);
    }
    private void AddMaxHPUI(float hpGained)
    {
        GameManager.Instance.UiManager.PlayerHud.HealthBar.AddMaxHp(hpGained, true);
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
        playerDash.CanDash = false;

    }
    public void UnLockPlayer()
    {
        playerController.ResetVelocity();
        PlayerController.CanMove = true;
        PlayerAbilityHandler.CanCast = true;
        PlayerMeleeAttack.CanAttack = true;
        playerDash.CanDash = true;
    }

    public void PlayerRespawn()
    {
        playerController.AnimBlender.SetBool("Die", false);
        UnLockPlayer();
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
    }

    private void EquipEmptyAbility()
    {
        GameManager.Instance.UiManager.PlayerHud.AbilityIcon.ResetAbilityImage();
        PlayerAbilityHandler.EquipSpell(null);
    }

    [SerializeField] private Attack testAttack;
    [ContextMenu("kill player")]

    public void KillPlayerTest()
    {
        Damageable.TakeDamage(testAttack);
    }
}
