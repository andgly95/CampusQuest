using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoRigidbody))]
	public class BattlePlayer : MonoBehaviour {

		public static PlayerController playerController;
		public float speed = 2.0f;
		private float upperBound = 50f;
		private float lowerBound = -10f;
		private float rightBound = 100f;
		private float leftBound = -200f;

		public GameObject player;
		private GameObject weapon;

		private bool isGrounded;
		private bool isAttacking = false;

		IsoRigidbody _isoRigidbody = null;


		void Start() {
			_isoRigidbody = GetComponent<IsoRigidbody>();
			if ( !_isoRigidbody ) {
				throw new UnityException("PlayerController. IsoRigidbody component not found!");
			}

			GameObject player = GameObject.FindGameObjectsWithTag ("Player") [0];
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");// Player singleton manager
			if (players.Length > 1) {
				foreach (GameObject p in players) {
					if (p != player) {
						Destroy (p);
					}
				}
			}

			weapon = GameObject.FindGameObjectWithTag ("Weapon");
		}

		void Update () {
			var velocity = _isoRigidbody.velocity;

			//Movement//

			velocity.x = 0;
			velocity.y = 0;

			// could not get wall colliders to work with this perspective. Instead I put bounds on where the player can move.
			// similarly we have to implement the same bounds for enemies in most cases

			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				if (player.transform.position.x <= leftBound) {
					velocity.x = 0;
					velocity.y = 0;
				} else{
					velocity.x += -0.8f * speed;
					velocity.y += 0.8f * speed;
				}
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				if (player.transform.position.x >= rightBound) {
					velocity.x = 0;
					velocity.y = 0;
				} else {
					velocity.x += 0.8f * speed;
					velocity.y += -0.8f * speed;				
				}
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				if (player.transform.position.y <= lowerBound) {
					velocity.x = 0;
					velocity.y = 0;
				} else {
					velocity.y += -0.8f * speed;
					velocity.x += -0.8f * speed;
				}
			}
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {

				if (player.transform.position.y >= upperBound) {
					velocity.x = 0;
					velocity.y = 0;
				} else {
					velocity.y += 0.8f * speed;
					velocity.x += 0.8f * speed;
				}
			}
				
			if (Input.GetButtonDown ("Jump") && isGrounded) {
				velocity.z += 2.5f*speed;
			}


			


			
			// Hitting
			if (Input.GetButtonDown ("Fire1")) {
				Debug.Log ("Swinging");

				isAttacking = true;
				weapon.SendMessage ("Attack");

			} else {
				isAttacking = false;
			}

			_isoRigidbody.velocity = velocity;
		}

		void OnIsoCollisionEnter(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Floors"){
				isGrounded = true;
			}
		}

		void OnIsoCollisionExit(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Floors"){
				isGrounded = false;
			}
		}
	}
}