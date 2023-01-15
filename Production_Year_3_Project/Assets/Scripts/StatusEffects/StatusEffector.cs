using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusEffector : MonoBehaviour
{
    /// <summary>
    /// invoked when this unit applys a status effect to an effectable
    /// </summary>
    public UnityEvent<StatusEffect> OnApplyStatusEffect;


}
