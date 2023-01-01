using UnityEngine;

public class CemuStatSheet : EnemyStatSheet
{
    [SerializeField] private float speedBosstDuarion;
    [SerializeField] private float speedBoostModifier;
    [SerializeField] private float decayingHealthAmout;
    [SerializeField] private float decayingHealthDuration;

    public float SpeedBosstDuarion { get => speedBosstDuarion; }
    public float SpeedBoostModifier { get => speedBoostModifier; }
    public float DecayingHealthAmout { get => decayingHealthAmout; }
    public float DecayingHealthDuration { get => decayingHealthDuration; }
}
