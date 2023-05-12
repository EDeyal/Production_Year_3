using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] float maxHPTest;
    [SerializeField] float testingNumber;
    [Button("UpdateHealth")]
    void UpdateHealth()
    {
        UpdateBar(testingNumber, true);
    }
    [Button("SetMaxHp")]
    void SetMaxHp()
    {
        SetHealthBar(maxHPTest);
        Debug.Log("Health bar max value: " + _healthBar.maxValue);
        Debug.Log("Health bar value: " + _healthBar.value);
    }
#endif

    float _currentHp;
    [SerializeField] float transitionDuration;
    [SerializeField] AnimationCurve addHealthBarCurve;
    [SerializeField] AnimationCurve reduceHealthBarCurve;
    [SerializeField] Slider _healthBar;
    [SerializeField] Color mainColor;
    public float CurrentHP => _currentHp;

    public void InitHealthBar(float maxHP)
    {
        _healthBar.maxValue = maxHP;
        _healthBar.value = 0;
    }
    public void SetHealthBar(float startingHP)
    {
        _currentHp = startingHP;
        _healthBar.maxValue = startingHP;
        _healthBar.value = _currentHp;
    }
    public void SetHealthBarAtZero(float startingHP)
    {
        _currentHp = 0;
        _healthBar.maxValue = startingHP;
        _healthBar.value = 0;
    }
    public void AddMaxHp(float addedAmount, bool replenishHealth = false)
    {
        _healthBar.maxValue = addedAmount;
        if (replenishHealth)
        {
            _healthBar.value += addedAmount;
        }
    }
    public void UpdateBar(float currentHp, bool hasTransition = true, AnimationCurve curve = null)
    {
        _currentHp = currentHp;
        if (hasTransition)
        {
            _healthBar.DOValue(currentHp, transitionDuration);
        }
        else
        {
            _healthBar.value = currentHp;
        }
    }
}

