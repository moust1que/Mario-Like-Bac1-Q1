using UnityEngine;

public static class RaycastExtension {
    public static bool Raycast(this Rigidbody rigidbody, Vector3 direction, float distance, float radius) {
		if(rigidbody.isKinematic)
			return false;
		
		RaycastHit hit;
		
		if(Physics.Raycast(rigidbody.position, direction, out hit, distance)) {
			Debug.DrawRay(rigidbody.position, direction * hit.distance, Color.green);
		}

		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}