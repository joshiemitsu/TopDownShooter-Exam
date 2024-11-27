using UnityEngine;

[CreateAssetMenu(fileName = "New UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public float Health;
    public float MaxHealth;
    public float MovementSpeed;
}