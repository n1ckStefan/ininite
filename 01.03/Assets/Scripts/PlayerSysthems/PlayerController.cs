using LaneSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        public Lane currentLane;

        [SerializeField] private LaneManager m_laneManager;
        [SerializeField] private float m_speed;

        private int m_laneIndex;
        private Coroutine m_moveCoroutine;
        #endregion

        #region Private Functions
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetNextLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetNextLane(1);
            }
            BounceOffset();
        }
        private void GetNextLane(int _direction)
        {
            if (m_laneIndex + _direction < 0)
            {
                return;
            }
            if (m_laneIndex + _direction >= m_laneManager.lanes.Length)
            {
                return;
            }
            m_laneIndex += _direction;
            MoveToNewLane(m_laneManager.lanes[m_laneIndex]);
            return;
        }

        private void BounceOffset()
        {
            if (m_moveCoroutine == null)
            {
                Vector3 bouncePosition = transform.position;
                bouncePosition.y = Mathf.Sin(Time.fixedTime * 15) * 0.15f;
                transform.position = bouncePosition;
            }
        }
        private IEnumerator PlayerLerp(Lane _targetLane)
        {
            float t = 0;
            Vector3 currentPos = transform.position;
            while (t <= 1)
            {
                t += Time.deltaTime * m_speed;
                Vector3 targetPosition = _targetLane.position;
                targetPosition.y = Mathf.Sin(1 - t) * 5;
                transform.position = Vector3.Lerp(currentPos, targetPosition, t);
                yield return null;
            }
            currentLane.playerOccupied = false;
            currentLane = _targetLane;
            currentLane.playerOccupied = true;
            m_moveCoroutine = null;
        }
        #endregion

        #region Public functions
        public void ResetPlayer()
        {
            currentLane.playerOccupied = false;
            m_laneIndex = m_laneManager.middleIndex;
            currentLane = m_laneManager.lanes[m_laneIndex];
            transform.position = currentLane.position;
            currentLane.playerOccupied = true;
        }
        public void MoveToNewLane(Lane _targetLane)
        {
            if (_targetLane == null)
            {
                return;
            }
            if (m_moveCoroutine != null)
            {
                StopCoroutine(m_moveCoroutine);
            }
            m_moveCoroutine = StartCoroutine(PlayerLerp(_targetLane));
        }
        #endregion
    }
}