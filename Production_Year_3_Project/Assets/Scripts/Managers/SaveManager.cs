using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SavePointHandler _savePointHandler;
    [SerializeField] RoomsManager _roomsManager;
    public SavePointHandler SavePointHandler => _savePointHandler;
    public RoomsManager RoomsManager { get => _roomsManager; set => _roomsManager = value; }


    public void LoadSave(int saveSlot)
    {
        //logic
    }
    public void SetSavePointHandler(SavePointHandler savePointHandler)
    {
        _savePointHandler = savePointHandler;
    }
}
