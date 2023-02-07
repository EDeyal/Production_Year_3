using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Random Movement SO", menuName = "ScriptableObjects/Random/RandomMovement")]

public class RandomMovementSO : ScriptableObject
{
    //[Range(-10,10)]
    public Vector2Int RandomDirection;

    [MinMaxSlider(0,10,true)]
    public Vector2 RandomLength;
}
