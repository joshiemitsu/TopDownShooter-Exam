using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int m_enemiesKilled;
    public int EnemyKilled
    {
        get { return m_enemiesKilled; }
        set { m_enemiesKilled = value; }
    }
}
