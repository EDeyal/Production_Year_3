using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [TabGroup("General")]
    [SerializeField] private Damageable damageable;
    [TabGroup("General")]
    [SerializeField] private StatSheet statSheet;
    public Damageable Damageable { get => damageable; }
    public StatSheet StatSheet { get => statSheet; }
    public virtual void Awake()
    {
        StatSheet.InitializeStats();
    }
}
