using LaneSystem;
using ObstacleSystem;
using PlayerSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private LaneSystem.LaneManager m_laneManager;
    [SerializeField] private ObstacleManager m_obstacleManager;
    [SerializeField] PlayerController m_playerController;
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

    private void InitialiseGame()
    {
        if (m_laneManager != null)
        {
            m_laneManager.InitialiseLanes();
        }
        ResetGameState();
        GameStarted();
    }

    private void GameStarted()
    {
        if (m_obstacleManager != null)
        {
            m_obstacleManager.StartObstacles();
        }
        GameEnded();
    }

    private void GameEnded()
    {

    }

    private void ResetGameState()
    {
        m_playerController.ResetPlayer();
        m_obstacleManager.WipeObstacles();
    }
    #endregion
}