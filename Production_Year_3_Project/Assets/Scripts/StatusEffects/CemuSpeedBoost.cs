using System.Collections;
using UnityEngine;

public class CemuSpeedBoost : StatusEffect
{
    //get buff time from manager
    public override void StartEffect()
    {
        base.StartEffect();
        if (host is BaseEnemy)
        {
            //((BaseEnemy)host).EnemyStatSheet.OverrideSpeed(get amount from manager);
            //host.StartCoroutine(BuffDurationTimer());
        }

    }

    public override void Reset()
    {

    }

    protected override void Subscribe()
    {
        //nothing to sub to here
    }

    protected override void UnSubscribe()
    {
        //nothing to unsub from here
    }

  /*  IEnumerator BuffDurationTimer()
    {
        yield return new WaitForSecondsRealtime(*//*get buff time from manager*//*);
        Remove();
    }*/
}
