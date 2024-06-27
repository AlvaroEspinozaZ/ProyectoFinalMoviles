using UnityEngine;

[CreateAssetMenu(fileName = "WarriorAlfilStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorAlfilStrategy", order = 1)]
public class WarriorAlfilStrategy : StrategySO
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
    public override void Instantiate(Vector3 tmpinit)
    {
        base.Instantiate(tmpinit);
    }
}
