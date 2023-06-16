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
    
    public string name => _name;
    public AudioClip audioClip => _audioClip;
    // bool loop
    //pitch
    //float volume
}
