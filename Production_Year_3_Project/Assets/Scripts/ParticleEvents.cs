using UnityEngine;

public class ParticleEvents : MonoBehaviour
{
    private void Awake()
    {
        var particle = GetComponent<ParticleSystem>().main;
        particle.stopAction = ParticleSystemStopAction.Disable;
    }
}
