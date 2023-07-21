using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "Sound", menuName = "NewSound")]
public class SoundSO : ScriptableObject
{
    [Range(0, 255)]
    int integerRange;
    [Header("Our settings")]
    [SerializeField] private string _name;
    [Space(5)]
    [Header("Audio Source General Settings")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _mute;
    [SerializeField] private bool _byPassEffect;
    [SerializeField] private bool _byPassListener;
    [SerializeField] private bool _byPassReverbZone;
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private bool _isLooping;
    [SerializeField,Range(0,255)] private int _priority;
    [SerializeField, Range(0f, 1f)] private float _volume;
    [SerializeField] private float _pitch;
    [SerializeField, Range(0f, 1.1f)] private float _reverbZoneMix;
    [Space(5)]
    [Header("3D Settings")]
    [SerializeField, Range(0f, 1f)] private float _spatialBlend;
    [SerializeField, Range(0, 360)] private float _dopplerLevel;
    [SerializeField] AudioRolloffMode _rollOffMode;
    [Header("minDistance must be smaller then maxDistance by Atleast 1%")]
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;




    public string name => _name;
    public AudioClip audioClip => _audioClip;
    public bool mute => _mute;                                      
    public bool byPassEffect => _byPassEffect;                     
    public bool byPassListener => _byPassListener;                
    public bool byPassReverbZone => _byPassReverbZone;           
    public bool playOnAwake => _playOnAwake;                    
    public bool isLooping => _isLooping;
    public float priority => _priority;
    public float volume => _volume;
    public float pitch => _pitch;
    public float reverbZoneMix => _reverbZoneMix;           
    public float spatialBlend => _spatialBlend;             
    public float dopplerLevel => _dopplerLevel;                
    public AudioRolloffMode rollOffMode => _rollOffMode;        
    public float minDistance => _minDistance;                 
    public float maxDistance => _maxDistance;                 

}
