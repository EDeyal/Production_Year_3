using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private int lastSpeed;

    public void SetFloat(string name, int givenSpeed)
    {
        anim.SetFloat(name, givenSpeed);
    }


    public void SetBool(string name, bool state)
    {
        anim.SetBool(name, state);
    }

    public void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }
}
