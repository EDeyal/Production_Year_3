using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : BaseCharacter
{
    protected override void SetUp()
    {
        base.SetUp();
        StatSheet.InitializeStats();
    }

}
