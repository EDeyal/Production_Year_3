using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHandler : MonoBehaviour, ICheckValidation
{
#if UNITY_EDITOR

    [TabGroup("Test"), SerializeField] float _maxHpTest;//with this the game will know how many health bars to create
    [TabGroup("Test"), SerializeField] float _currentHealthTest;
    [TabGroup("Test"), SerializeField] float _testAmount;
    [TabGroup("Test"), SerializeField] bool _replenishHealthTest;
    [TabGroup("Test"), SerializeField] bool _hasTransitionTest;
    [TabGroup("Test"), Button("InitHealthBars")]
    void InitializeHealthBars()
    {
        InitHealthBars(_maxHpTest);
    }
    [TabGroup("Test"), Button("Increase Max HP")]
    void IncreaceMaxHp()
    {
        AddMaxHp(_testAmount, _replenishHealthTest);
    }
    [TabGroup("Test"), Button("UpdateCurrentHealth")]
    void UpDateCurrentHealth()
    {
        UpdateHP(_currentHealthTest, _hasTransitionTest);
    }
    [TabGroup("Test"), Button("ResetHealthBars")]
    private void ResetBars()
    {
        ResetHealthBars(_maxHealthAmount);
    }

    [TabGroup("Test"), Button("SetEndPartBars")]
    private void SetEndPartLocation()
    {
        float amountOfBarsToCreate = Mathf.Ceil(_maxHealthAmount / _eachBarAmount);
        _healthBarEndPart.CalcDistance((int)amountOfBarsToCreate);
    }
#endif
    List<BaseHealthBar> _healthBars = new List<BaseHealthBar>();
    [SerializeField] float _eachBarAmount = 10;
    [SerializeField, ReadOnly] float _maxHealthAmount;
    [SerializeField, ReadOnly] float _currentHealth;
    [SerializeField, ReadOnly] int _healthBarItterator;
    [SerializeField] GameObject _healthBarPrefab;
    [SerializeField] Transform _healthBarContainer;
    [SerializeField] AnimationCurve _updateHealthBarCurve;
    [SerializeField] HealthBarEndPart _healthBarEndPart;
    Sequence _healthBarSequence;
    public void InitHealthBars(float maxHp)
    {
        _maxHealthAmount = maxHp;
        _currentHealth = _maxHealthAmount;
        //calculate how many health bars need to be created
        float amountOfBarsToCreate = Mathf.Ceil(_maxHealthAmount / _eachBarAmount);
        _healthBarEndPart.CalcDistance((int)amountOfBarsToCreate);
        amountOfBarsToCreate -= _healthBars.Count;
        Debug.Log("Amount of healthBarsToCreate: " + amountOfBarsToCreate);

        float amountToAdd = _maxHealthAmount;
        for (int i = 0; i < amountOfBarsToCreate; i++)
        {
            var hpBar = Instantiate(_healthBarPrefab, _healthBarContainer);
            var healthBarInstance = hpBar.GetComponent<BaseHealthBar>();
            healthBarInstance.InitHealthBar(_eachBarAmount);
            //last bar
            if (amountToAdd < _eachBarAmount)
            {
                healthBarInstance.UpdateBar(amountToAdd, false);
                amountToAdd = 0;
            }
            //middle bar
            else
            {
                healthBarInstance.UpdateBar(_eachBarAmount);
                amountToAdd -= _eachBarAmount;
            }
            //add bar to list
            _healthBars.Add(healthBarInstance);
        }
        _healthBarItterator = _healthBars.Count - 1;//set the itterator to the last health bar
        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void AddMaxHp(float addedAmount, bool replenishHealth)
    {
        //create extra health bars
        _maxHealthAmount += addedAmount;
        float amountOfBarsToCreate = Mathf.Ceil(_maxHealthAmount / _eachBarAmount);
        _healthBarEndPart.CalcDistance((int)amountOfBarsToCreate);
        amountOfBarsToCreate -= _healthBars.Count;
        if (amountOfBarsToCreate < 0)
        {
            Debug.LogError("Health bar handler logic Error");
        }
        for (int i = 0; i < amountOfBarsToCreate; i++)
        {
            var hpBar = Instantiate(_healthBarPrefab, _healthBarContainer);
            var healthBarInstance = hpBar.GetComponent<BaseHealthBar>();
            healthBarInstance.InitHealthBar(_eachBarAmount);
            _healthBars.Add(healthBarInstance);
        }


        //heal needed health
        if (replenishHealth)
        {
            var newHealth = _currentHealth + addedAmount;
            UpdateHP(newHealth, true);
            //_healthBarItterator += (int) amountOfBarsToCreate;//set the itterator to the new location
        }

        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void UpdateHP(float currentHP, bool hasTransition = true)
    {
        if (currentHP == _currentHealth)
        {
            return;
        }
        if (currentHP <0)
        {
            currentHP = 0;
        }
        float healthDelta = currentHP - _currentHealth;
        _currentHealth = currentHP;
        if (_healthBarSequence != null)
        {
            _healthBarSequence.Kill();
        }
        _healthBarSequence = DOTween.Sequence();
        while (healthDelta != 0)
        {
            float healthAmountThatCanBeChanged = 0;
            float newBarHealth = _healthBars[_healthBarItterator].CurrentHP;
            //adding health
            if (healthDelta > 0)
            {
                healthAmountThatCanBeChanged = _eachBarAmount - _healthBars[_healthBarItterator].CurrentHP;
                if (healthAmountThatCanBeChanged >= healthDelta) //this is the last bar needed to be changed
                {
                    newBarHealth += healthDelta;
                    var tween = _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition, _updateHealthBarCurve);//make the whole change
                    if (tween != null)
                    {
                        _healthBarSequence.Append(tween);
                    }
                    healthDelta = 0;//finish while loop
                }
                //there is not enough in this bar so we need to change the next bar
                else
                {
                    healthDelta -= healthAmountThatCanBeChanged;
                    newBarHealth += healthAmountThatCanBeChanged;
                    var tween = _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition, _updateHealthBarCurve);//change the max amount
                    if (tween != null)
                    {
                        _healthBarSequence.Append(tween);
                    }
                    _healthBarItterator++;
                }
            }
            //decreacing health
            else if (healthDelta < 0)
            {
                healthAmountThatCanBeChanged = _healthBars[_healthBarItterator].CurrentHP;

                if (healthAmountThatCanBeChanged >= -healthDelta) //this is the last bar needed to be changed
                {
                    newBarHealth += healthDelta;
                    var tween = _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition, _updateHealthBarCurve);//make the whole change
                    if (tween != null)
                    {
                        _healthBarSequence.Append(tween);
                    }
                    healthDelta = 0;//finish while loop
                }
                //there is not enough in this bar so we need to change the next bar
                else
                {
                    healthDelta += healthAmountThatCanBeChanged;
                    newBarHealth -= healthAmountThatCanBeChanged;
                    var tween = _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition, _updateHealthBarCurve);//change the max amount
                    if (tween != null)
                    {
                        _healthBarSequence.Append(tween);
                    }
                    if (_healthBarItterator > 0)
                    {
                        _healthBarItterator--;
                    }
                }
            }
        }
        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void ResetHealthBars(float maxHealth)
    {
        foreach (var healthBar in _healthBars)
        {
            Debug.Log("Destroying game object " + healthBar.gameObject.name);
            Destroy(healthBar.gameObject);
        }
        _healthBars.Clear();
        InitHealthBars(maxHealth);
    }

    public void CheckValidation()
    {
        if (!_healthBarPrefab)
            throw new System.Exception("HealthBarHandler has no health bar prefab");
        if (!_healthBarContainer)
            throw new System.Exception("HealthBarHandler has no health bar container");
        if (_updateHealthBarCurve == null)
            throw new System.Exception("HealthBarHandler has no health bar curve");
        if (!_healthBarEndPart)
            throw new System.Exception("HealthBarHandler has no health bar end Part script");
    }
}
