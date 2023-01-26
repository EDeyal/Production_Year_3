using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QovaxStatSheet : EnemyStatSheet
{
    [SerializeField, FoldoutGroup("Locomotion")] private float chargeSpeed;
    public float ChargeSpeed => chargeSpeed;

}
