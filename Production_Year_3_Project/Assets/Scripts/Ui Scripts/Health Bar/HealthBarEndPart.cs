using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEndPart : MonoBehaviour,ICheckValidation
{
    [SerializeField] RectTransform _holder;
    [SerializeField] Image _healthEndPartSprite;
    //[SerializeField] Image _healthStartPartSprite;
    [SerializeField] RectTransform _spaceing;
    [SerializeField] GridLayoutGroup _gridLayoutGroup;
    [SerializeField] float _xOffset;
    private void Awake()
    {
        CheckValidation();
    }
    public void CalcDistance(int healthBarAmount)
    {
        float spaceing = _gridLayoutGroup.spacing.x;
        float objectLength = _gridLayoutGroup.cellSize.x;
        float halfDistance = objectLength / 2;
        float objectDistance = healthBarAmount * objectLength;
        objectDistance += healthBarAmount * spaceing;
        objectDistance -= halfDistance;
        objectDistance += _xOffset;
        _spaceing.sizeDelta = new Vector2(objectDistance, _spaceing.sizeDelta.y);
        SetHolder();
    }
    private void SetHolder()
    {
        float holderSize = _spaceing.sizeDelta.x;
       // holderSize += _healthStartPartSprite.rectTransform.sizeDelta.x;
        holderSize += _healthEndPartSprite.rectTransform.sizeDelta.x;
        _holder.sizeDelta = new Vector2(holderSize, _holder.sizeDelta.y);
    }

    public void CheckValidation()
    {
        if (!_healthEndPartSprite)
            throw new System.Exception("HealthBarEndPart has no end part sprite");
        if (!_spaceing)
            throw new System.Exception("HealthBarEndPart has no spacing object");
        if(!_gridLayoutGroup)
            throw new System.Exception("HealthBarEndPart has no grid layout group");
        if(!_holder)
            throw new System.Exception("HealthBarEndPart has no holder");

    }
}
