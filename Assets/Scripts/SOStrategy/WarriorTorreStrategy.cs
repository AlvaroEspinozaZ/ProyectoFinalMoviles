using UnityEngine;

[CreateAssetMenu(fileName = "WarriorTorreStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorTorreStrategy", order = 6)]
public class WarriorTorreStrategy : StrategySO
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
