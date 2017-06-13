using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoRigidbody))]
	public class PlayerController : MonoBehaviour {

		public float speed = 2.0f;
		public GameObject player;
		public LayerMask groundLayer;
		private bool isGrounded;

		IsoRigidbody _isoRigidbody = null;

		void DebugValues(){
			Debug.Log ("Player position:" + transform.position);
		}

		void Start() {
			_isoRigidbody = GetComponent<IsoRigidbody>();
			if ( !_isoRigidbody ) {
				throw new UnityException("PlayerController. IsoRigidbody component not found!");
			}
			//GroundCheckPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f);
		}

		void Update () {

			var velocity = _isoRigidbody.velocity;

			//Movement//

			velocity.x = 0;
			velocity.y = 0;

			if (Input.GetKey (KeyCode.LeftArrow)) {
				velocity.x += -0.8f*speed;
				velocity.y += 0.8f*speed;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				velocity.x += 0.8f*speed;
				velocity.y += -0.8f*speed;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				velocity.y += -0.8f*speed;
				velocity.x += -0.8f*speed;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				velocity.y += 0.8f*speed;
				velocity.x += 0.8f*speed;
			}
			if (Input.GetButtonDown ("Jump") && isGrounded && velocity.z <= 0) {
				velocity.z += 2f*speed;
			}

			_isoRigidbody.velocity = velocity;
		}

		void OnIsoCollisionEnter(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Floors"){
				isGrounded = true;
				Debug.Log ("isGrounded is " + isGrounded);
			}

			// Destroy Enemy
//			if(iso_collision.gameObject.tag == "Enemy")
//				iso_collision.gameObject.SendMessage("DestroyEnemy");
//			
		}

		void OnIsoCollisionExit(IsoCollision iso_collision) {
			if(iso_collision.gameObject.tag == "Floors"){
				isGrounded = false;
				Debug.Log ("isGrounded is " + isGrounded);
			}
		}
	}
}