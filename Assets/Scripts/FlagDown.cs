using UnityEngine;

public class FlagDown : MonoBehaviour {
    [SerializeField] private float m_descentSpeed = 2f;
    [SerializeField] private Transform m_endPosition;
    [SerializeField] private float m_playerMovementDistance = 5f;
    [SerializeField] private float m_delayBeforePlayerMove = 2f;

    public bool m_isFlagLowered = false;
    public bool m_playerControlsDisabled = false;
    private Rigidbody m_playerRigidbody;
    private MarioMovement m_playerMovementScript;
    // private Vector3 initialPosition;
    [SerializeField] private GameManager m_gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !m_isFlagLowered && !m_playerControlsDisabled) {
            LowerFlag();
            DisablePlayerControls(other.gameObject);
            Invoke("MovePlayerAfterDelay", m_delayBeforePlayerMove);
            m_gameManager.AddScore(1000);
        }
    }

    private void DisablePlayerControls(GameObject player) {
        m_playerMovementScript = player.GetComponent<MarioMovement>();
        if (m_playerMovementScript != null) {
            m_playerMovementScript.enabled = false;
            m_playerControlsDisabled = true;
            // initialPosition = player.GetComponent<Rigidbody>().position;
        }
    }

    private void MovePlayerAfterDelay() {
        if (m_playerControlsDisabled) {
            m_playerRigidbody = m_playerMovementScript.GetComponent<Rigidbody>();
        }
    }

    private void Update() {
        if (m_playerControlsDisabled) {
            if (m_playerRigidbody != null) {
                float moveSpeed = m_playerMovementDistance / m_delayBeforePlayerMove;
                float distanceCovered = moveSpeed * Time.deltaTime;
                m_playerRigidbody.MovePosition(m_playerRigidbody.position + new Vector3(distanceCovered, 0f, 0f));
                m_playerMovementDistance -= distanceCovered;

                if (m_playerMovementDistance <= 0.001f) {
                    m_playerControlsDisabled = false;
                }
            }
        }

        if (m_isFlagLowered) {
            LowerFlagAnimation();
        }
    }

    private void LowerFlag() {
        m_isFlagLowered = true;
    }

    private void LowerFlagAnimation() {
        if (transform.position.y > m_endPosition.position.y) {
            float step = m_descentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_endPosition.position, step);
        }
    }
}