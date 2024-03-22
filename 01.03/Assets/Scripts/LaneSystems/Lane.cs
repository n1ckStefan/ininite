using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaneSystem
{
    [Serializable]
    public class Lane
    {
        #region Variables
        public bool active;
        public bool playerOccupied = false;
        public Vector3 position;
        #endregion

        public Lane(bool _active, Vector3 _position)
        {
            active = _active;
            position = _position;
        }
    }
}