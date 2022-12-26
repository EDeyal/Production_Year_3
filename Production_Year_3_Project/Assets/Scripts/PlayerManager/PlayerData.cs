using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Damageable playerDamageable;
    [SerializeField] private DamageDealer playerDamageDealer;
    [SerializeField] private PlayerStatSheet playerStats;
    public Damageable PlayerDamageable { get => playerDamageable; }
    public DamageDealer PlayerDamageDealer { get => playerDamageDealer; }
    public PlayerStatSheet PlayerStats { get => playerStats; }
}
