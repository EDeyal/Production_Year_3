using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAbilityHandler : MonoBehaviour
{
    private Ability currentAbility;

    private bool canCast;

    private float currentRemainingCooldDown;
    public Ability CurrentAbility { get => currentAbility; }
    public bool CanCast { get => canCast; set => canCast = value; }
    public float CurrentRemainingCooldDown { get => currentRemainingCooldDown; }

    private float lastCastSpell;
    public UnityEvent<Ability> OnEquipAbility;
    public UnityEvent<Ability> OnCast;

    [SerializeField] Ability test;
    private void Start()
    {
        canCast = true;
        GameManager.Instance.InputManager.OnSpellCast.AddListener(CastAbility);
        EquipSpell(test);
    }
    public virtual void CastAbility()
    {
        if (!canCast || lastCastSpell > Time.time - currentAbility.CoolDown || ReferenceEquals(currentAbility, null))
        {
            return;
        }
        else if (currentAbility.TryCast())
        {
            currentAbility.Cast();
            currentRemainingCooldDown = currentAbility.CoolDown;
            OnCast?.Invoke(currentAbility);
            StartCoroutine(CountDownCoolDown());
            lastCastSpell = Time.time;
        }
      
    }

    private void ResetLastCastSpell()
    {
        lastCastSpell = currentAbility.CoolDown * -1;
    }

    public void EquipSpell(Ability givenAbility)
    {
        currentAbility = givenAbility;
        OnEquipAbility?.Invoke(givenAbility);
        ResetLastCastSpell();
    }


    public void OnKillStealSpellEvent(Damageable target, DamageHandler dmg)
    {
        if (target.Owner is not BaseEnemy)
        {
            return;
        }
        Ability droppedAbility = ((BaseEnemy)target.Owner).DroppedAbilityForPlayer;
        ParticleEvents particle = GameManager.Instance.ObjectPoolsHandler.AbiltiyStealParticle.GetPooledObject();
        particle.transform.position = target.transform.position;
        particle.gameObject.SetActive(true);
        EquipSpell(droppedAbility);
    }

    private IEnumerator CountDownCoolDown()
    {
        while (currentRemainingCooldDown > 0)
        {
            currentRemainingCooldDown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
