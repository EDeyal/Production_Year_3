[System.Serializable]
public abstract class BaseAIAction : IActionAI
{
    public abstract BaseAIActionSO BaseActionSO { get; set; }
    public abstract void Activate();
    public abstract void CheckValidation();
    public abstract void OnEndOfAction();
}
