using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;
	private float attackTimer = 0;
	private float attackCd = 0.3f;

	private Transform player;
	public IsoBoxCollider attackTrigger;

	void Awake() {
		attackTrigger.enabled = false;
	}


	// Use this for initialization
	void Start () {
		attackTrigger = this.gameObject.transform.GetChild (0).gameObject.GetComponent<IsoBoxCollider>();
		// finds the attackTrigger regardless of public reference
		//Debug.Log ("Attack Trigger is: " + attackTrigger);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.N) && !attacking) {
			attacking = true;
			attackTimer = attackCd;
			attackTrigger.enabled = true; 
		}

		if (attacking) {// This here is a cooldown manager on attacking
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		
		}

	}
}
