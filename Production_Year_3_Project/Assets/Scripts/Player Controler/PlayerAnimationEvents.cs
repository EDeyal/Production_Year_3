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

    public void PlayWalkSoundLeft()
    {
        GameManager.Instance.SoundManager.PlaySound("PlayerWalk1");
    }
    public void PlayWalkSoundRight()
    {
        GameManager.Instance.SoundManager.PlaySound("PlayerWalk2");
    }

    public void PlayJumpSound()
    {
        GameManager.Instance.SoundManager.PlaySound("PlayerJump");
    }

    public void PlayDashSound()
    {
        GameManager.Instance.SoundManager.PlaySound("PlayerDash");
    }

    public void PlayerDeathSound()
    {
        GameManager.Instance.SoundManager.PlaySound("PlayerDeath");
    }
}
