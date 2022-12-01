using UnityEngine;

[CreateAssetMenu(fileName = "MoveOnFloorSO",menuName = "AI/Move On Floor SO")]
[System.Serializable]
public class MoveFloorSO : BaseMoveSO
{
    [SerializeField] int _moveSpeed;
    [SerializeField] int _MoveRadius;
    [SerializeField] bool _canJump;
    public override int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public override int MoveRadius { get => _MoveRadius; set => _MoveRadius = value; }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void StopActivation()
    {
        throw new System.NotImplementedException();
    }
}
