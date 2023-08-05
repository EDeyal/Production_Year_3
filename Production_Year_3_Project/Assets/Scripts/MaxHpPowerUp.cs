using UnityEngine;

public class MaxHpPowerUp : PowerUp
{
    [SerializeField] private float maxHpAmount;
    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        GameManager.Instance.PlayerManager.Damageable.AddMaxHP(maxHpAmount);
        GameManager.Instance.UiManager.ProgressionPopUp.CollectNewProgression(ProgressionType.Health);
        //GameManager.Instance.PlayerManager.PlayProgressionParticle(_progressionColor);

    }
}
