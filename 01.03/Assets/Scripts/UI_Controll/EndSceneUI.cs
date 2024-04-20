using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    [SerializeField] private Button m_btnNewGame;
    [SerializeField] private Button m_btnMainMenu;
    [SerializeField] private Button m_btnQuit;
    [SerializeField] private TMPro.TMP_Text m_score;

    private void Start()
    {
        m_btnNewGame.onClick.AddListener(LoadMain);
        m_btnMainMenu.onClick.AddListener(LoadStart);
        m_btnQuit.onClick.AddListener(Quit);
        m_score.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
    }
    private void LoadMain()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadStart()
    {
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
