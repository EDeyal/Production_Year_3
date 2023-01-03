using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAbilityHandler : MonoBehaviour
{
    private Ability currentAbility;
    public Ability CurrentAbility { get => currentAbility;}

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
        /*if (Time.time - lastCastSpell < currentAbility.CoolDown || ReferenceEquals(currentAbility, null))
        {
            return;
        }*/
        currentAbility.Cast();
        lastCastSpell = Time.time;
      //  anim.SetTrigger(currentAbility.AnimationTrigger);

    }

    private void ResetLastCastSpell()
    {
        lastCastSpell = 0;
    }

    public void EquipSpell(Ability givenAbility)
    {
        currentAbility = givenAbility;
        OnEquipAbility?.Invoke(givenAbility);
        ResetLastCastSpell();
    }

}
