using UnityEngine;

public class CameraScrollHandler : MonoBehaviour {
	[SerializeField] private Transform m_mario;

	private void LateUpdate() {
		Vector3 cameraPosition = transform.position;
		cameraPosition.x = Mathf.Max(cameraPosition.x, m_mario.position.x);
		transform.position = cameraPosition;
	}
}