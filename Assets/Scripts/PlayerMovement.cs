using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
	private Rigidbody m_rigidbody;
	private Camera m_camera;

	private Vector2 m_velocity;

	[SerializeField] private float m_inputAxis, m_moveSpeed = 20.0f, m_maxJumpHeight = 4.5f, m_maxJumpTime = 1.0f, lastX = 0.0f;
	private float m_jumpForce => 2.0f * m_maxJumpHeight / (m_maxJumpTime / 2.0f);
	private float m_gravity => -2.0f * m_maxJumpHeight / Mathf.Pow(m_maxJumpTime / 2.0f, 2);

	private bool m_grounded, m_jumping;

	private void Awake() {
		m_rigidbody = GetComponent<Rigidbody>();
		m_camera = Camera.main;
	}

	private void FixedUpdate() {
		Vector2 position = m_rigidbody.position;
		position += m_velocity * Time.fixedDeltaTime;

		Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
		Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		position.x = Mathf.Clamp(position.x, leftEdge.x + transform.localScale.x / 2.0f, rightEdge.x - transform.localScale.x / 2.0f);

		// if(m_velocity.x != 0.0f)
		// 	lastX = transform.position.x;
		// else
		// 	position.x = lastX;

		m_rigidbody.MovePosition(position);
	}

	private void Update() {
		HandleHorizontalMovement();

		m_grounded = m_rigidbody.Raycast2(Vector2.down);

		if(m_grounded)
			HandleVerticalMovement();

		ApplyGravity();
	}

	private void HandleHorizontalMovement() {
		m_inputAxis = Input.GetAxisRaw("Horizontal");
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
	
		if(transform.position.y > 1.0f) {
			m_velocity.y += m_gravity * multiplier * Time.deltaTime;
			m_velocity.y = Mathf.Max(m_velocity.y, m_gravity / 2.0f);
		}
		if(m_grounded && !m_jumping)
			m_velocity.y = 0.0f;
	}
}