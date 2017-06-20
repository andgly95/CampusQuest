using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoRigidbody))]
	public class PlayerController : MonoBehaviour {
		
		public static PlayerController playerController;
		private IsoWorld _IsoWorld;
		public float speed = 2.0f;
		//public GameObject player;
		private bool isGrounded;
		private bool isAttacking = false;
		GameObject player;

		private GameObject weapon;


		IsoRigidbody _isoRigidbody = null;
		/*
		void IsoWorldInit(){
			if (_IsoWorld != null) {
				DontDestroyOnLoad (_IsoWorld);
				//DontDestroyOnLoad (player);
				//set the gameController to this instance of the GameController class
				playerController = this;
			} else if (playerController != this) { // maybe this else if condition is wrong and/or condition to destroy _IsoWorld should go elsewhere
				Debug.Log ("Is this getting called? ");
				Destroy (_IsoWorld);
			} 

		}

		void IsoWorldDelete(){
			Transform[] children = _IsoWorld.GetComponentsInChildren<Transform> ();
			foreach (Transform t in children) {
				//Debug.Log (t.gameObject);
				if (!(t.gameObject.CompareTag ("Player") || t.gameObject.CompareTag ("PlayerSprite") || t.gameObject.CompareTag ("IsoWorld")))
					Destroy (t.gameObject);
			}
			//Debug.Log (children);
		}
		void IsoDestroyer(Transform IsoWorld){
			Transform[] children = _IsoWorld.GetComponentsInChildren<Transform> ();
			Debug.Log ("Length of empty IsoWorld children " + children.Length);

		}

		IsoWorld GetWorld(){
			return GetComponentInParent<IsoWorld>();
		}*/

		void Awake(){// Player Singleton Manager
			
		}

		void Start() {
			_isoRigidbody = GetComponent<IsoRigidbody>();
			if ( !_isoRigidbody ) {
				throw new UnityException("PlayerController. IsoRigidbody component not found!");
			}

			GameObject player = GameObject.FindGameObjectsWithTag ("Player") [0];
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");// Player singleton manager
			Debug.Log("Length of player list is: " + players.Length);
			if (players.Length > 1) {
				foreach (GameObject p in players) {
					if (p != player) {
						Debug.Log ("P is " + p);
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

			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				velocity.x += -0.8f*speed;
				velocity.y += 0.8f*speed;
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				velocity.x += 0.8f*speed;
				velocity.y += -0.8f*speed;
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				velocity.y += -0.8f*speed;
				velocity.x += -0.8f*speed;
			}
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				velocity.y += 0.8f*speed;
				velocity.x += 0.8f*speed;
			}
			if (Input.GetButtonDown ("Jump") && isGrounded) {
				velocity.z += 1.5f*speed;
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