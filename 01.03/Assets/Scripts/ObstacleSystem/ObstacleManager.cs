using LaneSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleSystem
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Variables


        [SerializeField] private AnimationCurve m_baseGameSpeed;
        [SerializeField] private float m_speedScalar;
        [SerializeField] private LaneManager m_laneManager;
        [SerializeField] private GameObject[] m_objectsToSpawn;
        [SerializeField] private Transform m_spawnParent;

        private Coroutine spawnRoutine;
        #endregion

        #region Private Functions
        //Spawn method is used to pick a random chunk object from the objectsToSpawn list and spawn it on the appropriate place
        private void Spawn()
        {
            Vector3 position = GetSpawnFromLane(m_laneManager.lanes[m_laneManager.middleIndex]);
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(m_objectsToSpawn[Random.Range(0, m_objectsToSpawn.Length)], position, rotation, m_spawnParent);
        }
        //This function gets the position of the lane to spawn on
        Vector3 GetSpawnFromLane(Lane _laneToSpawnOn)
        {
            Vector3 output = Vector3.zero;
            if (_laneToSpawnOn != null)
            {
                output = _laneToSpawnOn.position;
            }
            output.z = 100;
            return output;
        }

        //This code is used to keep calling the spawn method and essentialy keep spawning objects every time a given chunk prefab exits a given box collider
        private void OnTriggerExit(Collider collision)
        {
            Debug.Log(collision.tag);
            if (collision.tag == "Chunk")
            {
                Debug.Log("Collison With Chunk");
                Spawn();
            }
        }
        #endregion

        #region public Functions
        //This method is being called in the game manager to spawn the first obstacle
        //The OnTriggerExit method takes care of the rest of the spawning
        public void StartObstacles()
        {
            Spawn();
        }
        //Called when the game needs to be reset, the WipeObstacles method destroys every obstacle that has currently been spawned
        public void WipeObstacles()
        {
            List<GameObject> objectsToDelete = new List<GameObject>();
            for (int i = 0; i < m_spawnParent.childCount; i++)
            {
                objectsToDelete.Add(m_spawnParent.GetChild(i).gameObject);
            }

            foreach (var obstacle in objectsToDelete)
            {
                Destroy(obstacle);
            }
        }

        //Method to get the speed at which the obstacles have to move
        public float GetCurrentSpeed()
        {
            return m_baseGameSpeed.Evaluate(Mathf.Clamp(Time.fixedTime / 100000, 0.1f, 1)) * m_speedScalar;
        }
        #endregion
    }
}