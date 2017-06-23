using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class AttackTrigger : MonoBehaviour {

	public GameObject player;

	IsoRigidbody _isoRigidbody = null;

	void Start () {
		_isoRigidbody = player.GetComponent<IsoRigidbody>();
		if ( !_isoRigidbody ) {
			throw new UnityException("PlayerController. IsoRigidbody component not found!");
		}
	}

	void Update() {
		var velocity = _isoRigidbody.velocity;

		if (velocity.x < 0 && velocity.y > 0) {
			//Collider is to the left of the player
		} else if (velocity.x > 0 && velocity.y < 0) {
			//Collider is to the right of the player
		} else if (velocity.x > 0 && velocity.y > 0) {
			//Collider is "Above"/behind the player
		} else if (velocity.x < 0 && velocity.y < 0) {
			//Collider is "below"/ in front of the player
		} else if (velocity.x < 0) {
			// Colliider is bottom left of the player	
		} else if (velocity.y > 0) {
			// Collider is top left of the player
		} else if (velocity.x > 0) {
			// Collider is top right of the player
		} else if (velocity.y < 0) {
			// Collider is bottom right of the player
		}
		
	}

	void OnIsoTriggerEnter(IsoCollider collider){
		Debug.Log ("Triggered");
		if (collider.isTrigger != true && collider.CompareTag("Enemy")) {
			collider.SendMessageUpwards("DestroyEnemy");
		}
	}

}
