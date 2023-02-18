using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatSheet : StatSheet
{
    [SerializeField, FoldoutGroup("Locomotion")]
     float _knockbackSpeed;
    public float KnockbackSpeed => _knockbackSpeed;
}
