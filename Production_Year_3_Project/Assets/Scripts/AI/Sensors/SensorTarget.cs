using UnityEngine;
[CreateAssetMenu(fileName = "NewSensorTarget", menuName = "ScriptableObjects/Sensors/SensorTarget")]
public class SensorTarget : ScriptableObject
{
    public LayerMask LayerMask;

    public Color HitColor;
    public Color PartialHitColor;
    public Color MissColor;
}
