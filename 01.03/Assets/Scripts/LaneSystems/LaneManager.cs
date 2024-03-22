using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaneSystem
{
    public class LaneManager : MonoBehaviour
    {
        #region Variables
        public int middleIndex;
        [SerializeField] public Lane[] lanes;
        [SerializeField] public float m_laneSpacing;
        [SerializeField] private int m_laneNumber;
        #endregion


        #region Private Functions
        private void OnDrawGizmos()
        {
            for (int i = 0; i < lanes.Length; i++)
            {
                if (lanes[i].playerOccupied)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.grey;
                }
                Gizmos.DrawCube(lanes[i].position, new Vector3(0.25f, 0.25f, 100));
            }
        }
        #endregion


        #region Public Functions
        public void InitialiseLanes()
        {
            middleIndex = Mathf.FloorToInt(m_laneNumber / 2.0f);
            lanes = new Lane[m_laneNumber];
            for (int i = 0; i < lanes.Length; i++)
            {
                lanes[i] = new Lane(true, new Vector3((i - (middleIndex)) * m_laneSpacing, 0, 0));
            }
        }
        #endregion

    }
}