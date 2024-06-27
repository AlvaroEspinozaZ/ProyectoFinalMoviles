using UnityEngine;

[CreateAssetMenu(fileName = "WarriorCaballoStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorCaballoStrategy", order = 2)]
public class WarriorCaballoStrategy : StrategySO
{
    public override GameObject GetCharacter()
    {
        return base.GetCharacter();
    }
    public override int GetCostSpawn()
    {
        return base.GetCostSpawn();
    }
    public override int GetMaxSpawn()
    {
        return base.GetMaxSpawn();
    }
    public override float GetRechargeTime()
    {
        return base.GetRechargeTime();
    }
}
