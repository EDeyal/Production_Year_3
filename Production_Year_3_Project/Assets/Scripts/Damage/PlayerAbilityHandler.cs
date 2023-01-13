using UnityEngine;
using UnityEngine.Events;

public class PlayerAbilityHandler : MonoBehaviour
{
    private Ability currentAbility;

    private bool canCast;
    public Ability CurrentAbility { get => currentAbility; }
    public bool CanCast { get => canCast; set => canCast = value; }

    private float lastCastSpell;

    private Animator anim;

    public UnityEvent<Ability> OnEquipAbility;

    [SerializeField] Ability test;


    private void Start()
    {
        canCast = true;
        GameManager.Instance.InputManager.OnSpellCast.AddListener(CastAbility);
        EquipSpell(test);
    }
    public virtual void CastAbility()
    {
        if (!canCast ||lastCastSpell > Time.time - currentAbility.CoolDown || ReferenceEquals(currentAbility, null))
        {
            return;
        }
        currentAbility.Cast();
        lastCastSpell = Time.time;
        //  anim.SetTrigger(currentAbility.AnimationTrigger);

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

}
