using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBarUI m_healthBar;
    [SerializeField] private EnemiesKilledUI m_enemiesKilledUI;

    public HealthBarUI GetHealthBarUI()
    {
        return m_healthBar;
    }

    public EnemiesKilledUI GetEnemiesKilledUI()
    {
        return m_enemiesKilledUI;
    }
}
