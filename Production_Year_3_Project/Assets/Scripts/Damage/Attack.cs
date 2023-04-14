using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    [SerializeField] private DamageHandler damageHandler = new DamageHandler();
    [SerializeField] private List<TargetType> targetTypes = new List<TargetType>();

    public List<TargetType> TargetType { get => targetTypes; }
    public DamageHandler DamageHandler { get => damageHandler; set => damageHandler = value; }


    private void OnEnable()
    {
        damageHandler.ClearModifiers();
    }

    public bool CheckTargetValidity(TargetType givenTarget)
    {
        foreach (var item in targetTypes)
        {
            if (item == givenTarget)
            {
                return true;
            }
        }
        return false;
    }


}
