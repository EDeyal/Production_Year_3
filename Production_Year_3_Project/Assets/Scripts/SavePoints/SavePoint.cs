using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SavePoint : MonoBehaviour,ICheckValidation
{
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

    private void Start()
    {
        RegisterToSavePointHandler();
    }
    public void RegisterToSavePointHandler()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RegisterToSavePointHandler(this);
    }
    private void PlayParticles()
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
        PlayParticles();
    }
    public void DeactivateSavePoint()
    {
        _savePointAnimator.SetBool("IsActive",false);
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
    }
}
