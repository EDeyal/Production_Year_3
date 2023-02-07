using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealingCollider : MonoBehaviour
{
    [SerializeField] private DamageHandler damage = new DamageHandler();

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacter character = other.GetComponent<BaseCharacter>();
        if (ReferenceEquals(character, null))
        {
            return;
        }
        character.Damageable.Heal(damage);
        gameObject.SetActive(false);
    }
}
