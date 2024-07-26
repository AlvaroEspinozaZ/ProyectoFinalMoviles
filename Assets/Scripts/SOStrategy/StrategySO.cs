using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategySO : ScriptableObject
{
    [SerializeField] protected SpamArmy prefabBase;
    [SerializeField] protected Sprite profilePicture;
    [SerializeField] public int costSpawn;
    [SerializeField] public int maxSpawn;
    [SerializeField] public float rechargeTime;
    public int CostSpawm => costSpawn;
    public int MaxSpawn {get { return maxSpawn; } set { maxSpawn = value; }
    }
    public float RechargeTime => rechargeTime;
    public int cant => prefabBase.cant;
    public virtual void Instantiate(Vector3 tmpinit)
    {
        prefabBase.Instatiate(tmpinit);
    } 
    public virtual SpamArmy GetCharacter()
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
