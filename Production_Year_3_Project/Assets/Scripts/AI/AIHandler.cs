using UnityEngine;

public class AIHandler : MonoBehaviour, ICheckValidation
{
    [SerializeField] BaseAIAction[] _aiActions;
    public void Start()
    {
        CheckValidation();
    }
    public void CheckValidation()
    {
        if (_aiActions.Length == 0)
        {
            throw new System.Exception($"AIHandler:{gameObject.ToString()} has no actions");
        }
        else
        {
            foreach (var AIAction in _aiActions)
            {
                AIAction.CheckValidation();
            }
        }
    }
    public void StartActions()
    {

    }
    public void StopActions()
    {

    }
    public void ResetActions()
    {

    }
}
