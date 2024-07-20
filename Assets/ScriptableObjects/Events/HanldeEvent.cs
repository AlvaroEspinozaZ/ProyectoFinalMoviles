using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Events", menuName = "HanldeEvent/Events")]
public class HanldeEvent : ScriptableObject
{
    public event Action<Health> ActionHealth;
    public event Action<Health,int> ActionHealth2;
    public event Action<int> ActionInt;
    public event Action<float> ActionFloat;
    public event Action ActionGeneral;

    public void CallEventHealth(Health value)
    {
        ActionHealth?.Invoke(value);
    }
    public void CallEventHealth2(Health value,int damage)
    {
        ActionHealth2?.Invoke(value, damage);
    }
    public void CallEventInt(int value)
    {
        ActionInt?.Invoke(value);
    }
    public void CallEventFloat(float value)
    {
        ActionFloat?.Invoke(value);
    }
    public void CallEventGeneral()
    {
        ActionGeneral?.Invoke();
    }
}
