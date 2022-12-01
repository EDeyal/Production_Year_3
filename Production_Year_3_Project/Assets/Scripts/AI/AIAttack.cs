using UnityEngine;

[System.Serializable]
public class AIAttack : BaseAIAction
{
    [SerializeField] BaseAttackSO _baseAttackSO;
    public override BaseAIActionSO BaseActionSO { get => _baseAttackSO; set => _baseAttackSO = (BaseAttackSO)value; }

    public override void Activate()
    {
    }
    public override void CheckValidation()
    {
    }
    public override void OnEndOfAction()
    {
    }
}
