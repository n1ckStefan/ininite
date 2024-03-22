using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool m_active;

        private ObstacleManager m_obstaclesManager;
        private Rigidbody m_rb;
        #endregion

        #region Private Functions
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
            m_rb.isKinematic = true;
            m_rb.useGravity = false;

            GameObject go = GameObject.FindGameObjectWithTag("GameManager");
            m_obstaclesManager = go.GetComponentInChildren<ObstacleManager>();

        }
        #endregion

        #region Public Functions
        #endregion
    }
}