using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    [SerializeField] int maxJumps;
    [SerializeField] float jumpTime;
    int jumpsLeft;
    

    public int MaxJumps { get => maxJumps;}
    public float JumpTime { get => jumpTime; set => jumpTime = value; }

    private void Start()
    {
        ResetJumpsLeft();
    }

    public void ResetJumpsLeft()
    {
        jumpsLeft = maxJumps;
    }

    public void AddJump()
    {
        maxJumps++;
    }

    public bool IsExtraJumpAvailable()
    {
        if (jumpsLeft > 0)
        {
            return true;
        }
        return false;
    }


}
