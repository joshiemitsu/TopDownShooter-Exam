using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBarUI m_healthBar;
    [SerializeField] private EnemiesKilledUI m_enemiesKilledUI;
    [SerializeField] private GameStatusUI m_gameStatusUI;

    public HealthBarUI HealthBarUI => m_healthBar;
    public EnemiesKilledUI EnemiesKilledUI => m_enemiesKilledUI;
    public GameStatusUI GameStatusUI => m_gameStatusUI;
}
