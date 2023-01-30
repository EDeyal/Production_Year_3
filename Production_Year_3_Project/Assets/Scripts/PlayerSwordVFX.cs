using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem quovaxDashParticle;
    public void PlayQuovaxDashParticle()
    {
        quovaxDashParticle.gameObject.SetActive(true);
        quovaxDashParticle.Clear(true);
        quovaxDashParticle.Play(true);
    }

    public void StopQuovaxDashParticle()
    {
        quovaxDashParticle.gameObject.SetActive(false);
        quovaxDashParticle.Stop();
    }
}
