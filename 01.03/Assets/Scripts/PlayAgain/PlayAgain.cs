using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{
    [SerializeField] Button m_btnNewGame;
    private void Start()
    {
        m_btnNewGame.onClick.AddListener(LoadScene);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
