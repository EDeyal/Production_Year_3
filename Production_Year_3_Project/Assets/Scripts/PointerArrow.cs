using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{

    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    private float counter;

    private void Update()
    {
        if (counter >= 1)
        {
            SwitchColors();
            counter = 0;
        }
        rend.color = Color.Lerp(startColor, endColor, counter);
        counter += Time.deltaTime;
    }

    private void SwitchColors()
    {
        Color temp = startColor;
        startColor = endColor;
        endColor = temp;
    }



}
