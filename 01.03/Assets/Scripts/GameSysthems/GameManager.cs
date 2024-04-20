using LaneSystem;
using ObstacleSystem;
using PlayerSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private LaneSystem.LaneManager m_laneManager;
    [SerializeField] private ObstacleManager m_obstacleManager;
    [SerializeField] PlayerController m_playerController;
    [SerializeField] private ScoreManager m_scoreManager;
    #endregion

    #region Private Functions
    private void Start()
    {
        InitialiseGame();
    }

    //Temp 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitialiseGame();
        }
    }
    //Temp 
    //Method that starts the game
    public void InitialiseGame()
    {
        if (m_laneManager != null)
        {
            m_laneManager.InitialiseLanes();
        }
        ResetGameState();
        GameStarted();
        Time.timeScale = 1;
    }

    private void GameStarted()
    {
        if (m_obstacleManager != null)
        {
            m_obstacleManager.StartObstacles();
        }
        //GameEnded();
    }

    public void GameEnded()
    {
        SceneManager.LoadScene(2);
    }
    //Method that resets everything in order to restart the game
    private void ResetGameState()
    {
        m_playerController.ResetPlayer();
        m_obstacleManager.WipeObstacles();
        m_scoreManager.ResetScore();
    }
    #endregion
}