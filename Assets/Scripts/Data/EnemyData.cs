using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : UnitData
{
    public float AttackRange;
    public float AttackCooldown;
    public float AttackDamage;
}