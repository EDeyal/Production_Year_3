using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemuEnemy : GroundEnemy
{
    public int Distance; //plaster
    private void Update()
    {
        if (IsInRange(Distance))
        {
            //Fight
            Debug.Log("Cemu Fight");
        }
        else
        {
            Debug.Log("Cemu Idle");
            //Idle
        }

    }
}
