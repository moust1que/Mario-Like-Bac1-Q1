using UnityEngine;

public static class RaycastExtension {
    public static bool Raycast(this Rigidbody rigidbody, Vector3 direction, float distance, float radius) {
		LayerMask mask = LayerMask.GetMask("Player");

		if(rigidbody.isKinematic)
			return false;
		
		RaycastHit hit;
		
		Physics.SphereCast(rigidbody.position, radius, direction, out hit, distance, ~mask);

		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}