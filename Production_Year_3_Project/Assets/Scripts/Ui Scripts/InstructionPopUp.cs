using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionPopUp : MonoBehaviour,ICheckValidation
{
    [SerializeField] GameObject _instructionGameObject;
    [SerializeField] TextMeshProUGUI _instructionText;
    private void Start()
    {
        GameManager.Instance.UiManager.CacheInstructionPopUP(this);
        CloseInstructionText();
    }
    public void UpdateInstructionText(string text)
    {
        _instructionText.text = text;
        _instructionGameObject.gameObject.SetActive(true);
    }
    public void CloseInstructionText()
    {
        _instructionGameObject.SetActive(false);
        _instructionText.text = "";
    }

    public void CheckValidation()
    {
        if (_instructionGameObject == null)
            throw new System.Exception("Instruction pop up has no instructionGameObject");
    }
}
