using UnityEngine;

public class MaxHpPowerUp : PowerUp
{
    [SerializeField] private float maxHpAmount;
    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        GameManager.Instance.PlayerManager.Damageable.AddMaxHP(maxHpAmount);
    }
}