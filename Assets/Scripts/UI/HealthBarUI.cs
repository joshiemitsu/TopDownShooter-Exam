using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image m_foreground;

    public void SetHealth(float p_curHealth)
    {
        m_foreground.fillAmount = p_curHealth;
    }
}
