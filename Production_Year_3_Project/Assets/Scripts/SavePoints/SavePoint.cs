using Sirenix.OdinInspector;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [ReadOnly] public int ID;
    bool _canSave;
    public bool CanSave => _canSave;
    [SerializeField] public Transform _spawnPointTransform;
    public Transform SpawnPointTransform => _spawnPointTransform;
    [SerializeField] ParticleSystem _saveParticles;
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
}
