using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;
using IsoTools;
using IsoTools.Physics.Internal;

public class AttackTrigger : MonoBehaviour { 

	public static AttackTrigger attackTriggerScript;

	public GameObject player;

	IsoRigidbody _isoRigidbody = null;

	IsoObject _isoAttackCollider = null;
	IsoObject _isoPlayer = null;

	Vector3 boxPosition; // This one resets to the player position in update to avoid letting the collider run away

	public static bool battle;
	private int damage; // should be pulled from saved playerStats.

	public static GameObject overWorldEnemy;

	void Start () {
		_isoRigidbody = player.GetComponent<IsoRigidbody>();
		if ( !_isoRigidbody ) {
			throw new UnityException("PlayerController. IsoRigidbody component not found!");
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		_isoAttackCollider = GetComponent<IsoObject> ();
		_isoPlayer = player.GetComponent<IsoObject> ();
		damage = 10;//*** should be pulled from PlayerStats
	}

	void FixedUpdate() {
		var velocity = _isoRigidbody.velocity;
		boxPosition = _isoPlayer.position;
		if (velocity.x != 0 || velocity.y != 0) {
			if (velocity.x < 0 && velocity.y > 0) { // Left Strike
				//Collider is to the left of the player
				boxPosition.x += -0.15f;
				boxPosition.y += 0.25f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.x > 0 && velocity.y < 0) { // Right Strike
				//Collider is to the right of the player
				boxPosition.x += 0.25f;
				boxPosition.y += -0.15f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.x > 0 && velocity.y > 0) { // Top Strike
				//Collider is "Above"/behind the player
				boxPosition.x += 0.3f;
				boxPosition.y += 0.3f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.x < 0 && velocity.y < 0) { // Below Strike
				//Collider is "below"/ in front of the player
				boxPosition.x += -0.15f;
				boxPosition.y += -0.15f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.x < 0) { // Bottom-Left Strike
				// Colliider is bottom left of the player
				boxPosition.x += -0.3f;
				boxPosition.y += -0.1f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.y > 0) { // Top-Left Strike
				// Collider is top left of the player
				boxPosition.x += 0.1f;
				boxPosition.y += 0.3f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.x > 0) {// Top-Right Strike
				// Collider is top right of the player
				boxPosition.x += 0.3f;
				boxPosition.y += 0.1f;
				_isoAttackCollider.position = boxPosition;
			} else if (velocity.y < 0) {// Bottom-Right Strike
				// Collider is bottom right of the player
				boxPosition.x += -0.1f;
				boxPosition.y += -0.3f;
				_isoAttackCollider.position = boxPosition;
			}
		}
	}

	void OnIsoTriggerEnter(IsoCollider collider){
		if (!battle) {
			if (collider.isTrigger != true && collider.CompareTag ("Enemy")) {
				battle = true; // battle is switched back to false in the BattleManager Script after all enemies are defeated.
				overWorldEnemy = collider.gameObject; // this enemy should get deleted after the battle in the BattleManager Script 
			}

		} else {
			if (collider.isTrigger != true && collider.CompareTag ("Enemy")) {
				collider.SendMessageUpwards ("TakeDamage", damage);
			}
		}
	}


}

