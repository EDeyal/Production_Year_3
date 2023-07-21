using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class SavePoint : MonoBehaviour,ICheckValidation
{
    private static float _emissionOn = 1;
    private static float _emissionOff = 0;
    [ReadOnly] public int ID;
    bool _canSave;
    [Tooltip("Reference to the room the save point is located within")]
    [SerializeField] RoomHandler _savePointRoom;
    public RoomHandler SavePointRoom => _savePointRoom;
    public bool CanSave => _canSave;
    [SerializeField] public Transform _spawnPointTransform;
    public Transform SpawnPointTransform => _spawnPointTransform;
    [SerializeField] ParticleSystem _saveParticles;
    [SerializeField] Animator _savePointAnimator;
    [SerializeField] GameObject _savePointMatObject;
    private Renderer _savePointRenderer;
    [SerializeField] Color _checkPointColor;
    [SerializeField] AnimationCurve _colorInEase;
    [SerializeField] AnimationCurve _colorOutEase;
    [SerializeField] float _transitionDuration = 3.5f;

    private void Awake()
    {
        _savePointRenderer =  _savePointMatObject.GetComponent<Renderer>();
        if (_savePointRenderer == null)
        {
            Debug.LogError($"Save Point {gameObject.name} could not get renderer");
        }
        DeactivateSavePoint();
    }
    private void Start()
    {
        RegisterToSavePointHandler();
    }
    public void RegisterToSavePointHandler()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RegisterToSavePointHandler(this);
    }
    public void PlayParticles()
    {
        if (_saveParticles == null)
        {
            Debug.LogError($"SavePoint with ID: {ID} has no Particles on save");
            return;
        }
        //_saveParticles.Clear();
        _saveParticles.Play();
    }
    public void ActivateSavePoint()
    {
        _savePointAnimator.SetBool("IsActive",true);
            _savePointRenderer.material.DOColor(_checkPointColor * _emissionOn, "_EmissionColor",_transitionDuration).SetEase(_colorInEase);
        //_savePointMaterial.EnableKeyword("_EMISSION");
        //DynamicGI.SetEmissive(_savePointMaterial, _checkPointColor * _emissionOn);
    }
    public void DeactivateSavePoint()
    {
        _savePointAnimator.SetBool("IsActive",false);
            _savePointRenderer.material.DOColor(_checkPointColor * _emissionOff, "_EmissionColor", _transitionDuration).SetEase(_colorOutEase);
        // _savePointMaterial.EnableKeyword("_EMISSION");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _canSave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canSave = false;
        }
    }
    private void OnDisable()
    {
        ID = 0;
    }

    public void CheckValidation()
    {
        if (_savePointAnimator == null)
        {
            throw new System.Exception($"SavePoint {gameObject.name} has no animator controller");
        }
        if (_savePointMatObject ==null)
        {
            throw new System.Exception($"SavePoint {gameObject.name} has no mat reference");
        }
    }
}
