using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;
using IsoTools;
using IsoTools.Physics.Internal;
public class PlayerView : MonoBehaviour {

	private GameObject player;

	IsoRigidbody _isoRigidbody = null;

	IsoObject _isoViewCollider = null; 
	IsoObject _isoPlayer = null;

	Vector3 boxPosition; // This one resets to the player position in update to avoid letting the collider run away

	public static bool facingAnEnemy;
	public static GameObject facingThisEnemy; // Public Static because Enemy healthScript will reach into this script and grab is assigned value
	private Vector3 _isoPlayerPos;
	private Vector3 direction;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		_isoRigidbody = player.GetComponent<IsoRigidbody>();
		if ( !_isoRigidbody ) {
			throw new UnityException("PlayerController. IsoRigidbody component not found!");
		}

		_isoViewCollider = player.transform.GetChild (2).GetComponent<IsoObject> (); //large collider that serves to detect what is in front of the player
		_isoPlayer = player.GetComponent<IsoObject> ();
	}

	void FixedUpdate() {
		_isoPlayerPos = new Vector3 (_isoPlayer.position.x, _isoPlayer.position.y, _isoPlayer.position.z);

		var velocity = _isoRigidbody.velocity;
		boxPosition = _isoPlayer.position;
		if (velocity.x != 0 || velocity.y != 0) {
			if (velocity.x < 0 && velocity.y > 0) { // Left Strike
				//Collider is to the left of the player
				boxPosition.x += -0.15f;
				boxPosition.y += 1f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.x > 0 && velocity.y < 0) { // Right Strike
				//Collider is to the right of the player
				boxPosition.x += 1f;
				boxPosition.y += -0.15f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.x > 0 && velocity.y > 0) { // Top Strike
				//Collider is "Above"/behind the player
				boxPosition.x += 1.3f;
				boxPosition.y += 1.3f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.x < 0 && velocity.y < 0) { // Below Strike
				//Collider is "below"/ in front of the player
				boxPosition.x += 0f;
				boxPosition.y += 0f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.x < 0) { // Bottom-Left Strike
				// Colliider is bottom left of the player
				boxPosition.x += -0.3f;
				boxPosition.y += 0.35f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.y > 0) { // Top-Left Strike
				// Collider is top left of the player
				boxPosition.x += 0.6f;
				boxPosition.y += 1.5f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.x > 0) {// Top-Right Strike
				// Collider is top right of the player
				boxPosition.x += 1.5f;
				boxPosition.y += 1.1f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			} else if (velocity.y < 0) {// Bottom-Right Strike
				// Collider is bottom right of the player
				boxPosition.x += 0.9f;
				boxPosition.y += 0.2f;
				_isoViewCollider.position = boxPosition;
				direction = _isoRigidbody.velocity;
			}
		}
		Ray ray = new Ray(_isoPlayerPos, direction);
		IsoRaycastHit hit;
		//facingAnEnemy = IsoPhysics.Raycast(ray, out hit, max_distance: 5);
		facingAnEnemy = IsoPhysics.SphereCast(_isoPlayerPos, 0.3f, direction, out hit, max_distance: 3); // Spherecast is a thicc raycast

		facingThisEnemy = hit.collider.gameObject;
	}
}



