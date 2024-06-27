using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategySO : ScriptableObject
{
    [SerializeField] protected GameObject prefabBase;
    [SerializeField] protected Sprite profilePicture;
    [SerializeField] protected int costSpawn;
    [SerializeField] protected int maxSpawn;
    [SerializeField] protected float rechargeTime;
    public virtual GameObject GetCharacter()
    { 
        return prefabBase;
    }
    public virtual Sprite GetProfilePicture()
    {
        return profilePicture;
    }
    public virtual int GetCostSpawn()
    {
        return costSpawn;
    }
    public virtual int GetMaxSpawn()
    {
        return maxSpawn;
    }
    public virtual float GetRechargeTime()
    {
        return rechargeTime;
    }
}
