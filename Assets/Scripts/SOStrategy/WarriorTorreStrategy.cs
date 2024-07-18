using UnityEngine;

[CreateAssetMenu(fileName = "WarriorTorreStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorTorreStrategy", order = 6)]
public class WarriorTorreStrategy : StrategySO
{
    public override SpamArmy GetCharacter()
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
    public override void Instantiate(Vector3 tmpinit)
    {
        base.Instantiate(tmpinit);
    }
}
