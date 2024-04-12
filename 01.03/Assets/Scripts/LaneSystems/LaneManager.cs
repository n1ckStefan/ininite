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
        private int m_laneNumber = 3;
        #endregion


        #region Private Functions
        //Draws the lanes, for testing and editing purposes
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
        //This method calculates the middle index of the lanes array
        //It then loops through each index of the lanes array
        //And creates a new lane on each position
        public void InitialiseLanes()
        {
            middleIndex = Mathf.FloorToInt(m_laneNumber / 2.0f);
            lanes = new Lane[m_laneNumber];
            for (int i = 0; i < lanes.Length; i++)
            {
                lanes[i] = new Lane(true, new Vector3((i - (middleIndex)) * 2.7f, 0, 0));
            }
        }
        #endregion

    }
}