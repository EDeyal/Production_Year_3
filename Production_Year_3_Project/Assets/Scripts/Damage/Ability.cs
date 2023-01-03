using UnityEngine;

public class Ability : ScriptableObject
{
    private PlayerManager owner;

    [SerializeField] private float coolDown;
    [SerializeField] private string animationTrigger;


    public PlayerManager Owner { get => owner; }
    public float CoolDown { get => coolDown; }
    public string AnimationTrigger { get => animationTrigger; }

    public void CahceOwner(PlayerManager givenPlayer)
    {
        owner = givenPlayer;
    }

    public virtual void Cast()
    {

    }

}
