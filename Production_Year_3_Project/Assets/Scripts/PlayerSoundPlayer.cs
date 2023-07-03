using System.Collections;
using UnityEngine;

//shem tov
public class PlayerSoundPlayer : MonoBehaviour
{

    [SerializeField] private float walkSoundInterval;
    private float lastPlayed;
    public bool walking => GameManager.Instance.PlayerManager.PlayerController.GroundCheck.IsGrounded() && GameManager.Instance.PlayerManager.PlayerController.IsMoving;
    private bool right;

    private void Start()
    {
        GameManager.Instance.SoundManager.PlaySound("PHenvironment");
    }

    /* private void Update()
     {
         if (walking && Time.time - lastPlayed >= walkSoundInterval)
         {
             if (right)
             {
                 GameManager.Instance.SoundManager.PlaySound("PlayerWalking1");
             }
             else
             {
                 GameManager.Instance.SoundManager.PlaySound("PlayerWalking2");
             }
             right = !right;
             lastPlayed = Time.time;
         }
     }*/

}
