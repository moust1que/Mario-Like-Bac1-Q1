using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MarioMovement : MonoBehaviour {
	private Rigidbody m_rigidbody;
	private Camera m_camera;

	private Vector3 m_velocity;
	
	[SerializeField] private float m_moveSpeed = 8.0f,
								   m_maxJumpHeight = 4.0f,
								   m_maxJumpTime = 1.0f;
	[SerializeField] private float m_jumpForce => 2.0f * m_maxJumpHeight / (m_maxJumpTime / 2.0f);
	[SerializeField] private float m_gravity => -2.0f * m_maxJumpHeight / Mathf.Pow(m_maxJumpTime / 2.0f, 2);
	private float m_inputAxis;

	private bool m_grounded;
	private bool m_jumping;

	private void Awake() {
		m_rigidbody = GetComponent<Rigidbody>();
		m_camera = Camera.main;
	}

	private void Update() {
		HandleHorizontalMovement();

		m_grounded = m_rigidbody.Raycast(Vector3.down);
		Debug.Log(m_grounded);

		if(m_grounded)
			HandleVerticalMovement();

			ApplyGravity();
	}

	private void FixedUpdate() {
		Vector3 position = m_rigidbody.position;
		position += m_velocity * Time.fixedDeltaTime;

		Vector2 leftEge = m_camera.ScreenToWorldPoint(Vector2.zero);
		Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		position.x = Mathf.Clamp(position.x, leftEge.x + transform.localScale.x / 2.0f, rightEdge.x);

		m_rigidbody.MovePosition(position);
	}

	private void HandleHorizontalMovement() {
		m_inputAxis = Input.GetAxis("Horizontal");
		m_velocity.x = Mathf.MoveTowards(m_velocity.x, m_inputAxis * m_moveSpeed, m_moveSpeed * Time.deltaTime);
	}

	private void HandleVerticalMovement() {
		m_velocity.y = Mathf.Max(m_velocity.y, 0.0f);
		m_jumping = m_velocity.y > 0.0f;

		if(Input.GetButtonDown("Jump")) {
			m_velocity.y = m_jumpForce;
			m_jumping = true;
		}
	}

	private void ApplyGravity() {
		bool falling = m_velocity.y < 0.0f || !Input.GetButton("Jump");
		float multiplier = falling ? 2.0f : 1.0f;
	
		m_velocity.y += m_gravity * multiplier * Time.deltaTime;
		m_velocity.y = Mathf.Max(m_velocity.y, m_gravity / 2.0f);
	}

	// void OnDrawGizmos() {
	// 	Gizmos.color = Color.green;
	// 	Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2.0f, transform.position.z), transform.localScale.y / 2.0f);
	// }
}