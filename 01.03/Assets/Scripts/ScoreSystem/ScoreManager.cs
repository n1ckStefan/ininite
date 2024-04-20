using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int m_score;
    [SerializeField] private int m_highScore;
    [SerializeField] private TMPro.TMP_Text m_scoreUI;
    [SerializeField] private TMPro.TMP_Text m_highScoreUI;
    #endregion

    #region Private Functions
    private void Update()
    {
        m_scoreUI.text = m_score.ToString();
        m_highScoreUI.text = "HI: " + PlayerPrefs.GetInt("HighScore").ToString();
        if (m_score > PlayerPrefs.GetInt("HighScore"))
        {
            m_highScore = m_score;
            PlayerPrefs.SetInt("HighScore", m_highScore);
        }
        PlayerPrefs.SetInt("Score", m_score);
    }
    #endregion

    #region Public Functions
    public void AddScore(int _score)
    {
        m_score += _score;
    }

    public void ResetScore()
    {
        m_score = 0;
    }
    #endregion
}
