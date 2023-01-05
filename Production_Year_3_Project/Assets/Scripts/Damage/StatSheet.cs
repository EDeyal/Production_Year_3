using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
public class StatSheet : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Combat")] private float baseMaxHp;
    [SerializeField, FoldoutGroup("Combat")] private float invulnerabilityDuration;
    [SerializeField, FoldoutGroup("Combat")] private DecayingHealth decayingHealth = new DecayingHealth();
    [SerializeField, FoldoutGroup("Locomotion")] private float baseSpeed;

    public UnityEvent<float> OnOverrideSpeed;
    public UnityEvent<float> OnResetSpeed;

    private float currentSpeed;

    public virtual void InitializeStats()
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
        OnOverrideSpeed?.Invoke(givenSpeed);
    }

    public void ResetSpeed()
    {
        OnOverrideSpeed?.Invoke(baseSpeed);
    }
}
