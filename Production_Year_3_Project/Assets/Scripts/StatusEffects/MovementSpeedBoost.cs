using System.Collections;
using UnityEngine;

public class MovementSpeedBoost : StatusEffect
{
    //get buff time from manager
    Coroutine activeBuffRoutine;
    public override void StartEffect()
    {
        base.StartEffect();
        Boost speedBosst = host.GetBoostFromBoostType(BoostType.Speed);
        if (ReferenceEquals(speedBosst, null))
        {
            Debug.LogError("no speed boost given to " + host.name);
        }
        Debug.Log("starting speed boost on " + host.name);
        host.StatSheet.OverrideSpeed(speedBosst.BoostValue);
        activeBuffRoutine = host.StartCoroutine(BuffDurationTimer());
    }

    public override void Reset()
    {
        if (!ReferenceEquals(activeBuffRoutine, null))
        {
            host.StopCoroutine(activeBuffRoutine);
        }
        StartEffect();
    }

    protected override void Subscribe()
    {
        //nothing to sub to here
    }

    protected override void UnSubscribe()
    {
        //nothing to unsub from here
    }

    IEnumerator BuffDurationTimer()
    {
        yield return new WaitForSecondsRealtime((host.GetBoostFromBoostType(BoostType.Speed).BoostDuration));
        host.StatSheet.ResetSpeed();
        Remove();
    }
}
