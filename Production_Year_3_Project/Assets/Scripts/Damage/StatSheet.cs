using UnityEngine;
public class StatSheet : MonoBehaviour
{
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;

    private void Start()
    {
        InitializeStats();
    }

    protected virtual void InitializeStats()
    {
        GetComponent<Damageable>().SetStats(this);
    }

    public float CurrentHp { get => currentHp; }
    public float MaxHp { get => maxHp; }
}
