using UnityEngine;

public static class RaycastExtension {
	private static LayerMask m_layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody rigidbody, Vector3 direction) {
		if(rigidbody.isKinematic)
			return false;
		
		float radius = GameObject.FindGameObjectWithTag("Player").transform.localScale.y / 2.0f;
		float distance = GameObject.FindGameObjectWithTag("Player").transform.localScale.y / 2.0f;

		
		// RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, m_layerMask);
		RaycastHit hit;
		
		if(Physics.Raycast(rigidbody.position, direction, out hit, distance)) {
			Debug.DrawRay(rigidbody.position, direction * hit.distance, Color.green);
			Debug.Log("did hit");
		}

		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}