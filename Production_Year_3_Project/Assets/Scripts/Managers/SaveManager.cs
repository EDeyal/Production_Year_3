using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SavePointHandler _savePointHandler;
    public SavePointHandler SavePointHandler => _savePointHandler;

    public void LoadSave(int saveSlot)
    {
        //logic
    }
    public void SetSavePointHandler(SavePointHandler savePointHandler)
    {
        _savePointHandler = savePointHandler;
    }
}
