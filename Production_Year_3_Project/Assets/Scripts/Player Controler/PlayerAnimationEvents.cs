using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
   public void TurnOnMeleeAttackCollider()
    {
        GameManager.Instance.PlayerManager.PlayerMeleeAttackCollider.MyCollider.enabled = true;
    }
    public void TurnOffMeleeAttackCollider()
    {
        GameManager.Instance.PlayerManager.PlayerMeleeAttackCollider.MyCollider.enabled = false;
    }
}
