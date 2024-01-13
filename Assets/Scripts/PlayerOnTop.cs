using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTop : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player"))
			other.transform.SetParent(this.transform);
	}

	private void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player"))
			other.transform.SetParent(null);
	}
}