//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoTriggerListener), typeof(IsoCollisionListener))]
	public class OverworldEnemy : MonoBehaviour {

		public float speed = 2.0f;
		public GameObject player;
		private float distToPlayer;
		IsoRigidbody _isoRigidbody = null;

		// Use this for initialization
		void Start () {
			//FindObjectOfType<IsoBoxCollider> ();
			_isoRigidbody = GetComponent<IsoRigidbody> ();
			if (!_isoRigidbody) {
				throw new UnityException ("EnemyController. IsoRigidbody component not found!");
			}
		}

		// Update is called once per frame
		void Update () {
			var velocity = _isoRigidbody.velocity;
			DistToPlayer (player);

			velocity.x = 0;
			velocity.y = 0;

			if (distToPlayer <= 60f) { // we check to see if the player is within range of the enemy
				// if the player is top right of the enemy
				Debug.Log (distToPlayer);
				if (transform.position.x < player.transform.position.x) {
					velocity.x += 0.8f*speed;
					velocity.y += -0.8f*speed;
				}
				if (transform.position.x > player.transform.position.x) {
					velocity.x += -0.8f*speed;
					velocity.y += 0.8f*speed;
				}
				if (transform.position.y < player.transform.position.y) {
					velocity.y += 0.8f*speed;
					velocity.x += 0.8f*speed;
				}
				if (transform.position.y > player.transform.position.y) {
					velocity.y += -0.8f*speed;
					velocity.x += -0.8f*speed;
				}
			}
			_isoRigidbody.velocity = velocity;

		}

		void OnIsoCollisionEnter(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Player"){
				Debug.Log ("Switch to battle scene!");
			}
		}

		// same function as above, we dont need this
		/*void OnCollisionEnter2D(Collision2D col) {
			//Debug.Log ("Collision2D");
		}*/

		void DistToPlayer(GameObject Player){
			Debug.Log ("DistToPlayer called");
			distToPlayer = Mathf.Sqrt (Mathf.Pow (transform.position.x - player.transform.position.x, 2f) + Mathf.Pow (transform.position.y - player.transform.position.y, 2f));
			Debug.Log (distToPlayer);
		}

	}
}