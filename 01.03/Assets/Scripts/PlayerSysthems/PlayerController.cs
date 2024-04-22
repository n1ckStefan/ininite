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
        [SerializeField] private ScoreManager m_sm;
        private int m_laneIndex;
        private GameManager m_gameManager;
        private Coroutine m_moveCoroutine;
        [SerializeField] private LayerMask m_groundLayer;
        private float m_groundCheckDistance = 0.1f;
        private Rigidbody m_playerRb;
        [SerializeField] GameObject m_ground;
        [SerializeField] GameObject m_ray;
        #endregion

        #region Private Functions
        private void Awake()
        {
            m_gameManager = FindObjectOfType<GameManager>();
            m_playerRb = GetComponent<Rigidbody>();
        }

        //If A is pressed take away 1 from the lanes index, if D is pressed add 1
        private void Update()
        {
            Debug.Log(Vector3.Distance(transform.position,m_ground.transform.position));
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetNextLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetNextLane(1);
            }

            if (Physics.Raycast(m_ray.transform.position, Vector3.down, m_groundCheckDistance, m_groundLayer) && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jump());
            }
        }



       
        //Jump Method
        private IEnumerator Jump()
        {
            Debug.Log("jump");
            // Apply jump force
            float time = 0f;
            float duration = 0.5f;
            Vector3 jumpPosition = transform.position;
            while (time < duration) 
            {
                time += Time.deltaTime;
                float jumpProgress = time / duration;
                float verticalOffset = Mathf.Sin(jumpProgress * Mathf.PI) * 0.03f;

                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 0.03f, jumpProgress);
                transform.position += Vector3.up * verticalOffset;
                
                yield return null;
            }
        }


        //This method is being called when A or D are pressed, it gets the next lae and moves the player there
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

        //This is the code that is responsible for the actual physical movement of the player
        //It moves them by a speed of t which is a result of Time.deltaTime and the m_speed variable
        private IEnumerator PlayerLerp(Lane _targetLane)
        {
            float t = 0;
            Vector3 currentPos = transform.position;
            Vector3 targetPosition = _targetLane.position;
            targetPosition.y = currentPos.y; // Maintain the same y-coordinate

            while (t <= 1)
            {
                t += Time.deltaTime * m_speed;
                transform.position = Vector3.Lerp(currentPos, targetPosition, t);
                yield return null;
            }

            // Ensure the object reaches exactly the target position
            transform.position = targetPosition;

            currentLane.playerOccupied = false;
            currentLane = _targetLane;
            currentLane.playerOccupied = true;
            m_moveCoroutine = null;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                m_playerRb.AddForce(Vector3.up * 50, ForceMode.Impulse);
                Time.timeScale = 0.25f;
                StartCoroutine(TriggerDelay());
            }
            if (other.gameObject.tag == "Coin")
            {
                Destroy(other.gameObject);
                m_sm.AddScore(5);
            }
        }

        private IEnumerator TriggerDelay()
        {
            yield return new WaitForSeconds(0.1f);
            m_gameManager.GameEnded();
        }
        #endregion

        #region Public functions
        //The function below takes cae of reseting the lane hat the player is currently on and moving them in the middle lane, if they're not there already
        public void ResetPlayer()
        {
            currentLane.playerOccupied = false;
            m_laneIndex = m_laneManager.middleIndex;
            currentLane = m_laneManager.lanes[m_laneIndex];
            transform.position = currentLane.position;
            currentLane.playerOccupied = true;
        }
        //This code checks if the lane the player is trying to move to is valid and moves them to the target lane if it is
        //The method prevents the player from moving to a non existing lane
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