using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
[CreateAssetMenu(fileName = "Sound", menuName = "NewSound")]
public class SoundSO : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private string _name;
    [SerializeField] private bool _isLooping;
    [SerializeField] private float _pitch;
    [SerializeField,Range(0f,1f)] private float _volume;

    
    public string name => _name;
    public AudioClip audioClip => _audioClip;
    public bool isLooping => _isLooping;
    public float pitch => _pitch;
    public float volume => _volume;
}
