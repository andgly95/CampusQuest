using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class WeaponCollision : MonoBehaviour {
	
	void Start() {
		Debug.Log("Start");
		//GroundCheckPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f);
	}

	void OnIsoCollisionEnter(IsoCollision iso_collision){

		if(iso_collision.gameObject.tag == "Enemy"){ 
			Debug.Log ("Hit");
		}
	}
}
