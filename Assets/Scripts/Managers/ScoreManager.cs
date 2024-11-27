using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const int ENEMY_KILLS_TO_WIN = 10;

    [SerializeField] private int m_enemiesKilled;

    public int EnemyKilled
    {
        get { return m_enemiesKilled; }
        set 
        { 
            m_enemiesKilled = value; 

            if(m_enemiesKilled >= ENEMY_KILLS_TO_WIN)
            {
                GameManager.Instance.SetPlayerWin();
            }
        }
    }
}
