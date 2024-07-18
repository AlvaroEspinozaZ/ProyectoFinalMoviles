using UnityEngine;

[CreateAssetMenu(fileName = "WarriorCaballoStrategy", menuName = "Scriptable Objects/WarriorStrategy/WarriorCaballoStrategy", order = 2)]
public class WarriorCaballoStrategy : StrategySO
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
