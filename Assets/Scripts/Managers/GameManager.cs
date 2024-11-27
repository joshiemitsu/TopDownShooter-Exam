using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] Player m_player;
    [SerializeField] UIManager m_uiManager;
    [SerializeField] ScoreManager m_scoreManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public Player GetPlayer()
    {
        return m_player;
    }

    public UIManager GetUIManager()
    {
        return m_uiManager;
    }

    public ScoreManager GetScoreManager()
    {
        return m_scoreManager;
    }
}
