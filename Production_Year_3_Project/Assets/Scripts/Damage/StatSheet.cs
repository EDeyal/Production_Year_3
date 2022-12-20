using UnityEngine;
public class StatSheet : MonoBehaviour
{
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;

    public float CurrentHp { get => currentHp; }
    public float MaxHp { get => maxHp; }
}
