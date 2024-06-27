using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InformationSO", menuName = "Scriptable Objects/Information/CharacterInfo", order = 1)]
public class CharacterInfoSO : ScriptableObject
{
    public StrategySO[] selectedWarriors = new StrategySO[3];
    public StrategySO StrategySODefault;
    public void LoadInformation()
    {
        StrategySODefault =selectedWarriors[0];
    }
}
