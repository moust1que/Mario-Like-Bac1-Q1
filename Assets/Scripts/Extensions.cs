using UnityEngine;
using UnityEngine.UIElements;

public static class Extensions {
	private static LayerMask m_layerMask = LayerMask.GetMask("Default");
    public static bool Raycast2(this Rigidbody rigidbody, Vector2 direction) {
		if(rigidbody.isKinematic)
			return false;

		float radius = 0.25f;
		float distance = 0.75f;

		RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position - new Vector3(rigidbody.position.x, distance, rigidbody.position.z), radius, direction, 0.0f, m_layerMask);
		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}