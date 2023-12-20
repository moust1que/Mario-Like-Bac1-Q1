using UnityEngine;

public static class RaycastExtension {
    public static bool Raycast(this Rigidbody rigidbody, Vector3 direction, float distance, float radius) {
		if(rigidbody.isKinematic)
			return false;
		
		RaycastHit hit;
		
		// if(Physics.Raycast(rigidbody.position, direction, out hit, distance)) {
		// 	Debug.DrawRay(rigidbody.position, direction * hit.distance, Color.green);
		// }
		// if(Physics.SphereCast(rigidbody.position, radius, direction, out hit, distance)) {
		// 	Debug.DrawRay(rigidbody.position, direction * hit.distance, Color.green);
		// }
		// if(Physics.SphereCast(new Vector3(rigidbody.position.x, rigidbody.position.y - 0.5f / 2.0f, rigidbody.position.z), radius, direction, out hit, distance)) {
		// 	Debug.DrawRay(new Vector3(rigidbody.position.x, rigidbody.position.y - 0.5f / 2.0f, rigidbody.position.z), direction * hit.distance, Color.green);
		// }
		Physics.SphereCast(rigidbody.position, radius, direction, out hit, distance);
		// Physics.Raycast(rigidbody.position, direction, out hit, distance);

		return hit.collider != null && hit.rigidbody != rigidbody;
	}
}