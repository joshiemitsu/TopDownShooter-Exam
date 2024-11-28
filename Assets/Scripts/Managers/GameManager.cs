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

    [SerializeField] private Player m_player;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private ScoreManager m_scoreManager;
    [SerializeField] private SpawnPoolManager m_spawnPoolManager;

    [SerializeField] private GameState m_gameState;

    public Player Player => m_player;
    public UIManager UIManager => m_uiManager;
    public ScoreManager ScoreManager => m_scoreManager;
    public SpawnPoolManager SpawnPoolManager => m_spawnPoolManager;

    public GameState GameState => m_gameState;

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

    public void SetPlayerLose()
    {
        m_gameState = GameState.PLAYER_LOSE;
        m_uiManager.GameStatusUI.SetPlayerWin(false);
    }

    public void SetPlayerWin()
    {
        m_gameState = GameState.PLAYER_WIN;
        m_uiManager.GameStatusUI.SetPlayerWin(true);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
