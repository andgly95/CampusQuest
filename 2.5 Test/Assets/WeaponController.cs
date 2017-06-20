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

		//weapon.localScale += new Vector3 (10, 10, 0);
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
		//Debug.Log ("Hit");

		if (iso_collider.gameObject.CompareTag ("Enemy") && weaponAnimator.GetBool ("Swing"))
			Destroy (iso_collider.gameObject);
	}
}
