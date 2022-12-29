using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Damageable playerDamageable;
    [SerializeField] private DamageDealer playerDamageDealer;
    [SerializeField] private PlayerStatSheet playerStats;
    [SerializeField] private StatusEffectable playerStatusEffectable;
    [SerializeField] private StatusEffector playerStatusEffector;
    public Damageable PlayerDamageable { get => playerDamageable; }
    public DamageDealer PlayerDamageDealer { get => playerDamageDealer; }
    public PlayerStatSheet PlayerStats { get => playerStats; }
    public StatusEffectable PlayerStatusEffectable { get => playerStatusEffectable; }
    public StatusEffector PlayerStatusEffector { get => playerStatusEffector; }
}
