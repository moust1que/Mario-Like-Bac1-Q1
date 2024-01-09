using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIMove : MonoBehaviour {
	private Rigidbody m_rigidbody;
    [SerializeField] private float m_moveSpeed = 15.0f;
	private bool m_direction = true;

	private void Start() {
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update() {
		HandleHorizontalMovement();
	}

	private void HandleHorizontalMovement() {
		transform.Translate(Vector3.right * (m_direction ? m_moveSpeed : -1.0f * m_moveSpeed) * Time.deltaTime);
		if(Physics.Raycast(m_rigidbody.position, m_direction == true ? Vector3.right : Vector3.left, gameObject.transform.localScale.x / 2.0f)) {
			if(m_direction)
				m_direction = false;
			else
				m_direction = true;
		}
	}
}