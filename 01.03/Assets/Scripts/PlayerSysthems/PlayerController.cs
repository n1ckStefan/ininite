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
        private float m_jumpForce = 0.5f;
        [SerializeField] private LayerMask m_groundLayer;
        private float m_groundCheckDistance = 2f;
        private Rigidbody m_playerRb;

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
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetNextLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetNextLane(1);
            }

            RaycastHit m_hit;
            if (Physics.Raycast(transform.position, Vector3.down, out m_hit, m_groundCheckDistance, m_groundLayer) && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            
            BounceOffset();
        }

        //Jump Method
        void Jump()
        {
            Debug.Log("jump");
            // Apply jump force
            Vector3 bouncePosition = transform.position;
            bouncePosition.y = Mathf.Sin(1) * 3;
            transform.position = bouncePosition;


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

        private void BounceOffset()
        {
            if (m_moveCoroutine == null)
            {
                Vector3 bouncePosition = transform.position;
                bouncePosition.y = Mathf.Sin(Time.fixedTime * 15) * 0.13f;
                //transform.position = bouncePosition;
            }
        }
        //This is the code that is responsible for the actual physical movement of the player
        //It moves them by a speed of t which is a result of Time.deltaTime and the m_speed variable
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                m_playerRb.AddForce(0, 100, -100);
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