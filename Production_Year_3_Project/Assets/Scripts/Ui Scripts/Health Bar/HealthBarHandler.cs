using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
#if UNITY_EDITOR

    [TabGroup("Test"),SerializeField] float _maxHpTest;//with this the game will know how many health bars to create
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
#endif
    List<BaseHealthBar> _healthBars = new List<BaseHealthBar>();
    [SerializeField] float _eachBarAmount = 10;
    [SerializeField,ReadOnly] float _maxHealthAmount;
    [SerializeField,ReadOnly] float _currentHealth;
    [SerializeField,ReadOnly] int _healthBarItterator;
    [SerializeField] GameObject _healthBarPrefab;
    [SerializeField] Transform _healthBarContainer;
    [SerializeField] AnimationCurve _updateHealthBarCurve;
    public void InitHealthBars(float maxHp)
    {
        _maxHealthAmount = maxHp;
        _currentHealth = _maxHealthAmount;
        //calculate how many health bars need to be created
        float amountOfBarsToCreate = Mathf.Ceil(_maxHealthAmount / _eachBarAmount);
        amountOfBarsToCreate -= _healthBars.Count;
        Debug.Log("Amount of healthBarsToCreate: " + amountOfBarsToCreate);

        float amountToAdd = _maxHealthAmount;
        for (int i = 0; i < amountOfBarsToCreate; i++)
        {
            var hpBar = Instantiate(_healthBarPrefab,_healthBarContainer);
            var healthBarInstance = hpBar.GetComponent<BaseHealthBar>();
            healthBarInstance.InitHealthBar(_eachBarAmount);
            //last bar
            if (amountToAdd < _eachBarAmount)
            {
                healthBarInstance.UpdateBar(amountToAdd);
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
        _healthBarItterator = _healthBars.Count -1;//set the itterator to the last health bar
        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void AddMaxHp(float addedAmount, bool replenishHealth)
    {
        //create extra health bars
        _maxHealthAmount+=addedAmount;
        float amountOfBarsToCreate = Mathf.Ceil(_maxHealthAmount / _eachBarAmount);
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
            _currentHealth += addedAmount;
            UpdateHP(_currentHealth, true);
            _healthBarItterator += (int) amountOfBarsToCreate;//set the itterator to the new location
        }
        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void UpdateHP(float currentHP, bool hasTransition = false)
    {
        if (currentHP == _currentHealth)
        {
            return;
        }
        float healthDelta = currentHP - _currentHealth;

        while (healthDelta != 0)
        {
            float healthAmountThatCanBeChanged;
            float newBarHealth = _healthBars[_healthBarItterator].CurrentHP;
            //adding health
            if (healthDelta > 0)
            {
                healthAmountThatCanBeChanged = _eachBarAmount - _healthBars[_healthBarItterator].CurrentHP;
                if (healthAmountThatCanBeChanged >= healthDelta) //this is the last bar needed to be changed
                {
                    newBarHealth += healthDelta;
                    _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition);//make the whole change
                    healthDelta = 0;//finish while loop
                }
                //there is not enough in this bar so we need to change the next bar
                else
                {
                    healthDelta -= healthAmountThatCanBeChanged;
                    newBarHealth += healthAmountThatCanBeChanged;
                    _healthBars[_healthBarItterator].UpdateBar(newBarHealth,hasTransition);//change the max amount
                    _healthBarItterator++;
                }
            }
            //decreacing health
            else if (healthDelta < 0)
            {
                healthAmountThatCanBeChanged = _healthBars[_healthBarItterator].CurrentHP;
                
                if (healthAmountThatCanBeChanged >= -healthDelta) //this is the last bar needed to be changed
                {
                    newBarHealth -= healthDelta;
                    _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition);//make the whole change
                    healthDelta = 0;//finish while loop
                }
                //there is not enough in this bar so we need to change the next bar
                else
                {
                    healthDelta += healthAmountThatCanBeChanged;
                    newBarHealth -= healthAmountThatCanBeChanged;
                    _healthBars[_healthBarItterator].UpdateBar(newBarHealth, hasTransition);//change the max amount
                    _healthBarItterator--;
                }
            }
        }
        Debug.LogFormat("Health Bar Itterator is:" + _healthBarItterator);
    }
    public void ResetHealthBars(float maxHealth)
    {
        foreach (var healthBar in _healthBars)
        {
            Destroy(healthBar);
        }
        InitHealthBars(maxHealth);
    }
}
