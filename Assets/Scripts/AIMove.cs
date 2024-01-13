using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIMove : MonoBehaviour {
	private Rigidbody m_rigidbody;
    [SerializeField] private float m_moveSpeed = 15.0f;
	[SerializeField] private bool m_direction = true;

	private void Start() {
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update() {
		HandleHorizontalMovement();
	}

	private void HandleHorizontalMovement() {
		transform.Translate(Vector3.right * (m_direction ? m_moveSpeed : -1.0f * m_moveSpeed) * Time.deltaTime);
		if(Physics.Raycast(m_rigidbody.position, m_direction == true ? Vector3.right : Vector3.left, gameObject.transform.localScale.x / 2.0f + 0.1f)) {
			if(m_direction)
				m_direction = false;
			else
				m_direction = true;
			
			transform.GetChild(0).transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
		}
	}
}