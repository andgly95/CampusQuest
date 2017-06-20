//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoTriggerListener), typeof(IsoCollisionListener))]
	public class OverworldEnemy : MonoBehaviour {

		public float speed = 2.0f;
		private Transform player;
		private Vector3 spawnSpot;
		private float distToPlayer;
		IsoRigidbody _isoRigidbody = null;

		// Use this for initialization
		void Start () {
			//FindObjectOfType<IsoBoxCollider> ();
			_isoRigidbody = GetComponent<IsoRigidbody> ();
			if (!_isoRigidbody) {
				throw new UnityException ("EnemyController. IsoRigidbody component not found!");
			}

			spawnSpot = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GetPlayer ();
		}

		// Update is called once per frame
		void Update () {

			if (player == null) {
				GetPlayer ();
			}

			var velocity = _isoRigidbody.velocity;
			DistToPlayer (player);

			velocity.x = 0;
			velocity.y = 0;

			if (distToPlayer <= 60f) { // we check to see if the player is within range of the enemy
				// if the player is top right of the enemy
				//Debug.Log (distToPlayer);
				if (transform.position.x < player.transform.position.x) {
					velocity.x += 0.8f * speed;
					velocity.y += -0.8f * speed;
				}
				if (transform.position.x > player.transform.position.x) {
					velocity.x += -0.8f * speed;
					velocity.y += 0.8f * speed;
				}
				if (transform.position.y < player.transform.position.y) {
					velocity.y += 0.8f * speed;
					velocity.x += 0.8f * speed;
				}
				if (transform.position.y > player.transform.position.y) {
					velocity.y += -0.8f * speed;
					velocity.x += -0.8f * speed;
				}
			} else {
				if (transform.position.x < spawnSpot.x - 20) {
					velocity.x += 0.8f * speed;
					velocity.y += -0.8f * speed;
				}
				if (transform.position.x > spawnSpot.x + 20) {
					velocity.x += -0.8f * speed;
					velocity.y += 0.8f * speed;
				}
				if (transform.position.y < spawnSpot.y - 20) {
					velocity.y += 0.8f * speed;
					velocity.x += 0.8f * speed;
				}
				if (transform.position.y > spawnSpot.y + 20) {
					velocity.y += -0.8f * speed;
					velocity.x += -0.8f * speed;
				}
			}
			_isoRigidbody.velocity = velocity;

		}

		void OnIsoCollisionEnter(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Player"){
				Debug.Log ("Switch to battle scene!");
			}
		}

		void DestroyEnemy(){
			Destroy (gameObject);
		}

		// A method to DEBUG the OverworldEnemy object
		void SomeMethod(){
			Debug.Log("MSG");
		}

		void DistToPlayer(Transform player){
			distToPlayer = Mathf.Sqrt (Mathf.Pow (transform.position.x - player.transform.position.x, 2f) + Mathf.Pow (transform.position.y - player.transform.position.y, 2f));
		}

		void GetPlayer(){
			GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject> ();
			foreach (GameObject g in gObjs) {
				if (g.CompareTag ("Player")) {
					player = g.transform;
				}
			}
		}

	}
}