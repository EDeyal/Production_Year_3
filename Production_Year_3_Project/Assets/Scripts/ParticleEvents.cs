using UnityEngine;

public class ParticleEvents : MonoBehaviour
{
    ParticleSystem.MainModule particle;
    ParticleSystem mainParticle;
    private void Awake()
    {
        mainParticle = GetComponent<ParticleSystem>();
        particle = mainParticle.main;
        particle.stopAction = ParticleSystemStopAction.Disable;
    }

    private void OnEnable()
    {
        Debug.Log("restarting " + gameObject.name);
        mainParticle.Clear();
        mainParticle.Play();
    }
}
