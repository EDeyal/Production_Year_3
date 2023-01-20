using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAtkVFXTest : MonoBehaviour
{
    [SerializeField] private VisualEffect _vfx;
    public void PlayVFX()
    {
        _vfx.Play();
    }
}
