using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBarUI m_healthBar;

    public HealthBarUI GetHealthBarUI()
    {
        return m_healthBar;
    }
}
