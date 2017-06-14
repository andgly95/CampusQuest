using UnityEngine;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney {
	[RequireComponent(typeof(IsoRigidbody))]
	public class PlayerController : MonoBehaviour {

		public float speed = 2.0f;
		public GameObject player;
		private bool isGrounded;

		IsoRigidbody _isoRigidbody = null;

		void Start() {
			_isoRigidbody = GetComponent<IsoRigidbody>();
			if ( !_isoRigidbody ) {
				throw new UnityException("PlayerController. IsoRigidbody component not found!");
			}
		}

		void Update () {
			//isGrounded = IsoPhysics.Raycast(ray, out hit, groundCheckRadius, groundLayer, QueryTriggerInteraction.Collide);
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
}