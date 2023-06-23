using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    public AudioSource AudioSource { get => _audioSource;}
}
