using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : UnitData
{
    [SerializeField] private float m_rotationSpeed = 0;
    [SerializeField] private float m_fireRate = 0;

    public float RotationSpeed => m_rotationSpeed;
    public float FireRate => m_fireRate;
}