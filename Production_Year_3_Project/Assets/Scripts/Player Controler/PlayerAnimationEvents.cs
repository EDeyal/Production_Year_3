using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public void Attack()
    {
        GameManager.Instance.PlayerManager.PlayerMeleeAttack.MeleeAttackEvent();
    }

    public void FinishAttack()
    {
        GameManager.Instance.PlayerManager.PlayerMeleeAttack.CanAttack = true;
    }
}
