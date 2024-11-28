using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : UnitData
{
    [SerializeField] private float m_attackRange = 0;
    [SerializeField] private float m_attackCooldown = 0;
    [SerializeField] private float m_attackDamage = 0;

    public float AttackRange => m_attackRange;
    public float AttackCooldown => m_attackCooldown;
    public float AttackDamage => m_attackDamage;
}