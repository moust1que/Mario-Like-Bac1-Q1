using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MarioMovement : MonoBehaviour {
	[SerializeField] GameManager m_gameManager;
	private Rigidbody m_rigidbody;
	private Camera m_camera;

	private Vector3 m_velocity;
	
	public float m_moveSpeed = 20.0f;
	[SerializeField] private float m_maxJumpHeight = 4.5f,
								   m_maxJumpTime = 1.0f;
	[SerializeField] private float m_jumpForce => 2.0f * m_maxJumpHeight / (m_maxJumpTime / 2.0f);
	[SerializeField] private float m_gravity => -2.0f * m_maxJumpHeight / Mathf.Pow(m_maxJumpTime / 2.0f, 2);
	private float m_inputAxis;

	private bool m_grounded, m_hitLeft, m_hitRight, m_hitTop, m_jumping;

	private void Awake() {
		m_rigidbody = GetComponent<Rigidbody>();
		m_camera = Camera.main;
	}

	private void Update() {
		HandleHorizontalMovement();

		SendRaycast();

		if(m_grounded || m_hitTop)
			HandleVerticalMovement();

		ApplyGravity();
	}

	private void FixedUpdate() {
		Vector3 position = m_rigidbody.position;
		position += m_velocity * Time.fixedDeltaTime;

		Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
		Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		position.x = Mathf.Clamp(position.x, leftEdge.x + transform.localScale.x / 2.0f, rightEdge.x);

		m_rigidbody.MovePosition(position);
	}

	private void HandleHorizontalMovement() {
		m_inputAxis = Input.GetAxisRaw("Horizontal");
		m_velocity.x = Mathf.MoveTowards(m_velocity.x, m_inputAxis * m_moveSpeed / 1.5f, m_moveSpeed * Time.deltaTime);

        if(m_hitLeft && m_inputAxis < 0.0f || m_hitRight && m_inputAxis > 0.0f) {
            m_velocity.x = 0.0f;
        }

		if(Input.GetButtonDown("Sprint") && m_moveSpeed == 20.0f)
			m_moveSpeed += 10.0f;
		if(Input.GetButtonUp("Sprint") && m_moveSpeed == 30.0f)
			m_moveSpeed -= 10.0f;
	}

	private void HandleVerticalMovement() {
		m_velocity.y = Mathf.Max(m_velocity.y, 0.0f);
		m_jumping = m_velocity.y > 0.0f;

		if(Input.GetButtonDown("Jump")) {
			m_velocity.y = m_jumpForce;
			m_jumping = true;
		}
        if(m_jumping && m_hitTop)
            m_velocity.y = 0.0f;
	}

	private void ApplyGravity() {
		bool falling = m_velocity.y < 0.0f || !Input.GetButton("Jump");
		float multiplier = falling ? 2.0f : 1.0f;
	
        if(gameObject.transform.position.y > 1.0f) {
            m_velocity.y += m_gravity * multiplier * Time.deltaTime;
            m_velocity.y = Mathf.Max(m_velocity.y, m_gravity / 2.0f);
        }
	}

	private void SendRaycast() {
		// float verticalDistance = gameObject.GetComponent<CapsuleCollider>().height / 4.0f;
        // float horizontalDistance = gameObject.GetComponent<CapsuleCollider>().radius / 2.0f;
        // float verticalRadius = gameObject.GetComponent<CapsuleCollider>().height / 4.0f;
        // float horizontalRadius = gameObject.GetComponent<CapsuleCollider>().radius / 2.0f;
		float verticalDistance, horizontalDistance = 0.0f, radius = 0.5f;
		if(!m_gameManager.m_bigMario) {
			verticalDistance = 0.5f;
		}else {
			verticalDistance = 1.0f;
		}

		m_grounded = m_rigidbody.Raycast(Vector3.down, verticalDistance, radius);
        m_hitTop = m_rigidbody.Raycast(Vector3.up, verticalDistance, radius);
        m_hitLeft = m_rigidbody.Raycast(Vector3.left, horizontalDistance, radius);
        m_hitRight = m_rigidbody.Raycast(Vector3.right, horizontalDistance, radius);
	}

	public void setBigMario() {
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
		gameObject.GetComponent<CapsuleCollider>().height = 3.0f;
		gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 0.5f, 0.0f);
		m_gameManager.m_bigMario = true;
	}

	public void setSmallMario() {
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
		gameObject.GetComponent<CapsuleCollider>().height = 2.0f;
		gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 0.0f, 0.0f);
		m_gameManager.m_bigMario = false;
	}
}