using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Events", menuName = "HanldeEvent/Events")]
public class HanldeEvent : ScriptableObject
{
    public event Action<int> ActionGeneralInt;
    public event Action<float> ActionGeneralFloat;
    public event Action ActionGeneral;

    public void CallEventInt(int value)
    {
        ActionGeneralInt?.Invoke(value);
    }
    public void CallEventFloat(float value)
    {
        ActionGeneralFloat?.Invoke(value);
    }
    public void CallEventGeneral()
    {
        ActionGeneral?.Invoke();
    }
}
