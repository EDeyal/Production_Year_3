using UnityEngine;

[CreateAssetMenu(fileName = "CountDownAction", menuName = "ScriptableObjects/Actions/CountDown")]

public class CountDownAction : BaseAction<ActionCooldownData>
{
    public float CountTime;
    public override bool InitAction(ActionCooldownData countData)
    {
        if (!countData.CooldownTask.IsActive)
        {
            countData.CooldownTask.StartTimer(CountTime);
        }
        return countData.CooldownTask.CheckCompletion();
    }
}
