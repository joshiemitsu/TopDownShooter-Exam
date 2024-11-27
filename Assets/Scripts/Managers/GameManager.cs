using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    IN_GAME,
    PLAYER_WIN,
    PLAYER_LOSE
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] Player m_player;
    [SerializeField] UIManager m_uiManager;
    [SerializeField] ScoreManager m_scoreManager;
    [SerializeField] SpawnPoolManager m_spawnPoolManager;

    [SerializeField] GameState m_gameState;

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

    public SpawnPoolManager GetSpawnPoolManager()
    {
        return m_spawnPoolManager;
    }

    public GameState GetGameState()
    {
        return m_gameState;
    }

    public void SetPlayerLose()
    {
        m_gameState = GameState.PLAYER_LOSE;
        m_uiManager.GetGameStatusUI().SetPlayerWin(false);
    }

    public void SetPlayerWin()
    {
        m_gameState = GameState.PLAYER_WIN;
        m_uiManager.GetGameStatusUI().SetPlayerWin(true);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
