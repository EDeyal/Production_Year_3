using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour
{
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void DisableParticle()
    {
        particle.Stop();
    }

    public void RestartParticle()
    {   
        particle.Clear();
        particle.Play();
    }


}
