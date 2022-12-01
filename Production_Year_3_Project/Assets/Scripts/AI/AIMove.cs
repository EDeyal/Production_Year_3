[System.Serializable]
public class AIMove : BaseAIAction
{
    BaseMoveSO _baseMoveSO;
    public override BaseAIActionSO BaseActionSO { get => _baseMoveSO; set => _baseMoveSO = (BaseMoveSO)value; }

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
