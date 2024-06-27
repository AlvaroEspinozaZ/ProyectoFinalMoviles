using UnityEngine;

[CreateAssetMenu(fileName = "WarriorPeonStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorPeonStrategy", order = 3)]
public class WarriorPeonStrategy : StrategySO
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
