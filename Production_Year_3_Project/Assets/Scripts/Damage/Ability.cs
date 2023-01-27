using UnityEngine;


public class Ability : ScriptableObject
{
    private BaseCharacter owner;


    [SerializeField] private float coolDown;
    [SerializeField] private Sprite abilityArtwork;
    [SerializeField] private string animationTrigger;


    public BaseCharacter Owner { get => owner; }
    public float CoolDown { get => coolDown; }
    public string AnimationTrigger { get => animationTrigger; }

    public void CahceOwner(BaseCharacter givenCharacter)
    {
        owner = givenCharacter;
    }

    public virtual void Cast()
    {

    }

}
