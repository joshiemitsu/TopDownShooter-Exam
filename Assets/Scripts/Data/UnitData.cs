using UnityEngine;

[CreateAssetMenu(fileName = "New UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private float m_health;
    [SerializeField] private float m_maxHealth;
    [SerializeField] private float m_movementSpeed;

    public float Health => m_health;
    public float MaxHealth => m_maxHealth;
    public float MovementSpeed => m_movementSpeed;
}