using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private CCController playerController;
    [SerializeField] private AttackAnimationHandler playerMeleeAttackAnimationHandler;
    [SerializeField] private DamageDealingCollider playerMeleeAttackCollider;
    public CCController PlayerController { get => playerController; }
    public AttackAnimationHandler PlayerMeleeAttack { get => playerMeleeAttackAnimationHandler; }
    public DamageDealingCollider PlayerMeleeAttackCollider { get => playerMeleeAttackCollider;}
}
