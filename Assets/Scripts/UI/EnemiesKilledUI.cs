using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesKilledUI : MonoBehaviour
{
    [SerializeField] TMP_Text m_text;

    void Awake()
    {
        m_text = this.GetComponent<TMP_Text>();
    }

    public void SetText(int p_enemyKilled)
    {
        m_text.text = "" + p_enemyKilled;
    }
}
