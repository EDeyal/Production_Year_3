using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private BaseHealthBar healthBar;
    [SerializeField] private BaseHealthBar decayingHealthBar;
    [SerializeField] private AbilityImageTest abilityIcon;

    private void Start()
    {
        GameManager.Instance.UiManager.CachePlayerHud(this);
        GameManager.Instance.PlayerManager.SubscirbeUI();
    }
    public BaseHealthBar HealthBar { get => healthBar; }
    public BaseHealthBar DecayingHealthBar { get => decayingHealthBar; }
    public AbilityImageTest AbilityIcon { get => abilityIcon; }
}
