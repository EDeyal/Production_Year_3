using System;
using UnityEngine;
public class ActionCooldown
{
    float _cooldownTime = ZERO;
    float _currentCount = ZERO;
    bool _isActive = false;
    const float ZERO = 0;
    const float ONE = 1;
    public ActionCooldown()
    {
        _cooldownTime = ONE;
    }
    public ActionCooldown(float cooldownTime)
    { 
        _cooldownTime = cooldownTime;
    }
    public bool IsActive => _isActive;
    public void ResetCooldown()
    {
        _isActive = false;
        _currentCount = _cooldownTime;
    }
    public void StartTimer(float time)
    {
        _cooldownTime = time;
        _currentCount = _cooldownTime;
        _isActive = true;
    }
    public void CountDown()
    {
        _currentCount -= Time.deltaTime;
    }
    public bool CheckCompletion()
    {
        CountDown();
        if (_currentCount <= ZERO)
        {
            CooldownCompleted();
            return true;
        }
        return false;
    }
    void CooldownCompleted()
    {
        _isActive = false;
        _currentCount = _cooldownTime;
    }
}
