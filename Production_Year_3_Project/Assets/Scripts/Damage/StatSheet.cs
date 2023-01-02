using Sirenix.OdinInspector;
using UnityEngine;
public class StatSheet : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Combat")] private float baseMaxHp;
    [SerializeField, FoldoutGroup("Combat")] private float invulnerabilityDuration;
    [SerializeField, FoldoutGroup("Combat")] private DecayingHealth decayingHealth = new DecayingHealth();
    [SerializeField, FoldoutGroup("Locomotion")] private float baseSpeed;

    private float currentSpeed;

    private void Start()
    {
        InitializeStats();
    }

    protected virtual void InitializeStats()
    {
        GetComponent<Damageable>().SetStats(this);
        currentSpeed = baseSpeed;
    }

    public float MaxHp { get => baseMaxHp; }
    public float InvulnerabilityDuration { get => invulnerabilityDuration; }
    public float Speed { get => currentSpeed; }
    public DecayingHealth DecayingHealth { get => decayingHealth;}

    public void OverrideSpeed(float givenSpeed)
    {
        currentSpeed = givenSpeed;
    }

    public void ResetSpeed()
    {
        currentSpeed = baseSpeed;
    }
}
