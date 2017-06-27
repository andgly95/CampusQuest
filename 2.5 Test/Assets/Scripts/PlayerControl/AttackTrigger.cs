using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;
using IsoTools;
using IsoTools.Physics.Internal;

public class AttackTrigger : MonoBehaviour { 

	public GameObject player;

	IsoRigidbody _isoRigidbody = null;

	IsoObject _isoBoxCollider = null;
	IsoObject _isoPlayer = null;

	Vector3 boxPosition; // This one resets to the player position in update to avoid letting the collider run away

	private bool battle;
	private int damage;


	void Start () {
		_isoRigidbody = player.GetComponent<IsoRigidbody>();
		if ( !_isoRigidbody ) {
			throw new UnityException("PlayerController. IsoRigidbody component not found!");
		}
		_isoBoxCollider = GetComponent<IsoObject> ();
		_isoPlayer = player.GetComponent<IsoObject> ();
		damage = 10;
		//battle = false;	
	}

	void FixedUpdate() {
		var velocity = _isoRigidbody.velocity;
		boxPosition = _isoPlayer.position;
		if (velocity.x != 0 || velocity.y != 0) {
			if (velocity.x < 0 && velocity.y > 0) { // Left Strike
				//Collider is to the left of the player
				boxPosition.x += -0.15f;
				boxPosition.y += 0.25f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.x > 0 && velocity.y < 0) { // Right Strike
				//Collider is to the right of the player
				boxPosition.x += 0.25f;
				boxPosition.y += -0.15f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.x > 0 && velocity.y > 0) { // Top Strike
				//Collider is "Above"/behind the player
				boxPosition.x += 0.3f;
				boxPosition.y += 0.3f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.x < 0 && velocity.y < 0) { // Below Strike
				//Collider is "below"/ in front of the player
				boxPosition.x += -0.15f;
				boxPosition.y += -0.15f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.x < 0) { // Bottom-Left Strike
				// Colliider is bottom left of the player
				boxPosition.x += -0.3f;
				boxPosition.y += -0.1f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.y > 0) { // Top-Left Strike
				// Collider is top left of the player
				boxPosition.x += 0.1f;
				boxPosition.y += 0.3f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.x > 0) {// Top-Right Strike
				// Collider is top right of the player
				boxPosition.x += 0.3f;
				boxPosition.y += 0.1f;
				_isoBoxCollider.position = boxPosition;
			} else if (velocity.y < 0) {// Bottom-Right Strike
				// Collider is bottom right of the player
				boxPosition.x += -0.1f;
				boxPosition.y += -0.3f;
				_isoBoxCollider.position = boxPosition;
			}
		}
	}

	void OnIsoTriggerEnter(IsoCollider collider){
		if (!battle) {
			if (collider.isTrigger != true && collider.CompareTag ("Enemy")) {
				battle = true; //Need to work on transition between OverWorld and BattleScene so we can switch this back to false
						   //After Battle is over
			}

		} else {
			if (collider.isTrigger != true && collider.CompareTag ("Enemy")) {
				collider.SendMessageUpwards ("TakeDamage", damage);
			}
		}
	}
}

