using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] Button m_btnMainMenu;

    void Start()
    {
        m_btnMainMenu.onClick.AddListener(LoadStart);
    }

    private void LoadStart()
    {
        SceneManager.LoadScene(0);
    }
}
