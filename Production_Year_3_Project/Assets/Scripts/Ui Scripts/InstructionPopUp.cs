using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionPopUp : MonoBehaviour
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

}
