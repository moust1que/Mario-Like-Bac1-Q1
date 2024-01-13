using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IsPlayerCollisionExtension {
    public static bool IsPlayerCollision(this Collision collision) {
		Debug.Log(collision.gameObject.layer);
		LayerMask playerLayer = GameObject.Find("Mario").layer;
		return playerLayer.value == collision.gameObject.layer ? true : false;
	}
}