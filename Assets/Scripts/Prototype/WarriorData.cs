using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WarriorData", menuName = "Scriptable Objects/WarriorData")]
public class WarriorData : ScriptableObject
{
    public int characterHealth = 100;
    
    public float distanceAttack = 2.0f;
    public int damage = 10;
    public float intervalAttack = 1.5f;
    public float timeToDeath = 1.1f;
    public float speed = 2.0f;
    public StrategySO _strategy;
}
