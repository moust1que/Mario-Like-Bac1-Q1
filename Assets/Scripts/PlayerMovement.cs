using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
	private Camera m_camera;
	private Rigidbody m_rigidbody;

	[SerializeField] private Transform m_marioScale;

	private Vector3 m_velocity;

	[SerializeField] private float m_moveSpeed = 20.0f,
								   m_maxJumpHeight = 4.5f,
								   m_maxJumpTime = 1.0f;
	private float m_jumpForce => 2.0f * m_maxJumpHeight / (m_maxJumpTime / 2.0f);
	private float m_gravity => -2.0f * m_maxJumpHeight / Mathf.Pow(m_maxJumpTime / 2.0f, 2);

	private bool m_grounded, m_jumping, m_hitTop, m_hitLeft, m_hitRight;

	private void Awake() {
		m_camera = Camera.main;
		m_rigidbody = GetComponent<Rigidbody>();

		m_marioScale = transform.Find("Small");
		// m_collider.transform.localScale = m_marioScale.GetComponent<Collider>().transform.localScale;
	}

	private void Update() {
		ApplyGravity();
		HandleHorizontalMovement();

		SendRaycast();

		if(m_grounded)
			HandleVerticalMovement();

		// Debug.Log(m_collisions.Count);
	}

	// private void OnCollisionEnter(Collision collision) {
	// 	foreach(ContactPoint contact in collision.contacts) {
	// 		m_grounded = contact.normal.y > 0.8f ? true : false;
	// 	}
	// }

	private void FixedUpdate() {
		Vector3 position = m_rigidbody.position;
		position += m_velocity * Time.fixedDeltaTime;

		Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
		Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		position.x = Mathf.Clamp(position.x, leftEdge.x + transform.localScale.x / 2.0f, rightEdge.x);

		m_rigidbody.MovePosition(position);
	}

	private void HandleHorizontalMovement() {
		float inputAxis = Input.GetAxisRaw("Horizontal");
		m_velocity.x = Mathf.MoveTowards(m_velocity.x, inputAxis * m_moveSpeed / 1.5f, m_moveSpeed * Time.deltaTime);
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

		// if(m_grounded && !m_jumping)
		// 	m_velocity.y = 0.0f;
	}

	private void SendRaycast() {
		m_grounded = m_rigidbody.Raycast(Vector3.zero, 0.0f, m_rigidbody.transform.localScale.x / 2.0f);
	}
}