using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPopUP : MonoBehaviour
{
    [SerializeField] GameObject _popUP;
    void Start()
    {
        _popUP.SetActive(false);
        GameManager.Instance.PlayerManager.DamageDealer.OnKill.AddListener(OnFirstKill);   
    }
    void OnFirstKill(Damageable damageable,DamageHandler damageHandler)
    {
        if ( damageable.Owner is BaseEnemy)
        {
            GameManager.Instance.PlayerManager.DamageDealer.OnKill.RemoveListener(OnFirstKill);
            _popUP.SetActive(true);
            GameManager.Instance.InputManager.SetCurserVisability(true);
            GameManager.Instance.PauseGameTimeScale(true);
        }
    }
    public void ContinueGame()
    {
        GameManager.Instance.InputManager.SetCurserVisability(false);
        GameManager.Instance.PauseGameTimeScale(false);
        _popUP.SetActive(false);
    }

}
