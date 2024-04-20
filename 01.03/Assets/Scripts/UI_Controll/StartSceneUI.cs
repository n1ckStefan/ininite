using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] Button m_btnPlay;
    [SerializeField] Button m_btnOptions;
    [SerializeField] Button m_btnQuit;

    void Start()
    {
        m_btnPlay.onClick.AddListener(LoadMain);
        m_btnOptions.onClick.AddListener(LoadOptions);
        m_btnQuit.onClick.AddListener(Quit);
    }
    private void LoadMain()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadOptions()
    {
        SceneManager.LoadScene(3);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
