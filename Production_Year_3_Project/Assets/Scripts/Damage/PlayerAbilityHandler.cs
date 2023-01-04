using UnityEngine;
using UnityEngine.Events;

public class PlayerAbilityHandler : MonoBehaviour
{
    private Ability currentAbility;
    public Ability CurrentAbility { get => currentAbility; }

    private float lastCastSpell;

    private Animator anim;

    public UnityEvent<Ability> OnEquipAbility;

    [SerializeField] Ability test;


    private void Start()
    {
        GameManager.Instance.InputManager.OnSpellCast.AddListener(CastAbility);
        EquipSpell(test);
    }
    public virtual void CastAbility()
    {
        if (lastCastSpell > Time.time - currentAbility.CoolDown || ReferenceEquals(currentAbility, null))
        {
            return;
        }
        Debug.Log("casting " + currentAbility.name);
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
