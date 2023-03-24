using Sirenix.OdinInspector;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [ReadOnly] public int ID;
    bool _canSave;
    public bool CanSave => _canSave;
    [SerializeField] public Transform _spawnPointTransform;
    public Transform SpawnPointTransform => _spawnPointTransform;
    private void Start()
    {
        RegisterToSavePointHandler();
    }
    public void RegisterToSavePointHandler()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RegisterToSavePointHandler(this);
    }
    //private void Update()
    //{
    //    if (_canSave)
    //    {
    //        if (true)//if player presses a key
    //        { 
    //            GameManager.Instance.SaveManager.SavePointHandler.SetPlayerSavePoint(this);
    //        }
    //    }
    //}
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
