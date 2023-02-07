using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenFadeSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetHashCode() == GameManager.Instance.PlayerManager.gameObject.GetHashCode())
        {
            StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeToBlack());
        }
    }
}
