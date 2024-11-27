using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatusUI : MonoBehaviour
{
    [SerializeField] TMP_Text m_text;
    [SerializeField] Button m_restartButton;

    private const string PLAYER_WIN_TEXT = "PLAYER WIN!";
    private const string PLAYER_LOSE_TEXT = "PLAYER LOSE!";

    private void Start()
    {
        m_restartButton.onClick.RemoveAllListeners();
        m_restartButton.onClick.AddListener(RestartButtonPressed);
    }

    public void SetPlayerWin(bool p_didPlayerWin)
    {
        this.gameObject.SetActive(true);
        if(p_didPlayerWin)
        {
            m_text.text = PLAYER_WIN_TEXT;
        }
        else
        {
            m_text.text = PLAYER_LOSE_TEXT;
        }
    }

    public void RestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }
}
