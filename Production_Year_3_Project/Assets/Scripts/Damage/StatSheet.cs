using Sirenix.OdinInspector;
using UnityEngine;
public class StatSheet : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Combat")] private float maxHp;
    [SerializeField, FoldoutGroup("Combat")] private float invulnerabilityDuration;

    [SerializeField,FoldoutGroup("Locomotion")] private float speed;

    private void Start()
    {
        InitializeStats();
    }

    protected virtual void InitializeStats()
    {
        GetComponent<Damageable>().SetStats(this);
    }

    public float MaxHp { get => maxHp; }
    public float InvulnerabilityDuration { get => invulnerabilityDuration; }
    public float Speed { get => speed; }
}
