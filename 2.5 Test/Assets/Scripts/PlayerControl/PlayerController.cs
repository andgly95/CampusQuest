using UnityEngine;
using IsoTools.Physics;

public class PlayerController : MonoBehaviour {
	
	public static PlayerController playerController;
	public float speed = 2.0f;
	private bool isGrounded;

	public static GameObject player;
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
