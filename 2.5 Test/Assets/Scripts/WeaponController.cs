using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class WeaponController : MonoBehaviour {

	Transform weapon;

	private Animator weaponAnimator;

	void Awake() {
		weapon = GameObject.FindGameObjectWithTag ("Weapon").transform;
		Debug.Log ("Weapon: " + weapon);

	}

	// Use this for initialization
	void Start () {
		weaponAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		}
		

	void Attack(){
		if(!weaponAnimator.GetBool("Swing"))
			weaponAnimator.SetTrigger ("Swing");
	}

	void OnIsoTriggerEnter(IsoCollider iso_collider){
		if (iso_collider.gameObject.CompareTag ("Enemy") && weaponAnimator.GetBool ("Swing")) {
			Destroy (iso_collider.gameObject);
			Debug.Log ("Hit from trigger enter");
		}
	}

	void OnIsoTriggerStay(IsoCollider iso_collider){
		if (iso_collider.gameObject.CompareTag ("Enemy") && weaponAnimator.GetBool ("Swing")) {
			Destroy (iso_collider.gameObject);
			Debug.Log ("Hit from trigger stay");
		}
	}

}
