using LaneSystem;
//using ObstacleSystem;
using PlayerSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private LaneSystem.LaneManager m_laneManager;
    //[SerializeField] private ObstacleManager m_obstacleManager;
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
        Debug.Log("Init");
        if (m_laneManager != null)
        {
            m_laneManager.InitialiseLanes();
        }
        ResetGameState();
        GameStarted();
    }

    private void GameStarted()
    {
        Debug.Log("Started");
        //if (m_obstacleManager != null)
        //{
        //    m_obstacleManager.StartObstacles();
        //}
        GameEnded();
    }

    private void GameEnded()
    {
        Debug.Log("Ended");
    }

    private void ResetGameState()
    {
        Debug.Log("Reset");
        m_playerController.ResetPlayer();
        //m_obstacleManager.WipeObstacles();
    }
    #endregion

    #region Public Functions
    #endregion
}