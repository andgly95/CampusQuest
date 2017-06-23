using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;
	private float attackTimer = 0;
	private float attackCd = 0.3f;
	public IsoBoxCollider attackTrigger;

	private Animator weaponAnimator;

	void Awake() {
		attackTrigger.enabled = false;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.N) && !attacking) {
			attacking = true;
			attackTimer = attackCd;
			attackTrigger.enabled = true; 
			Debug.Log ("We are attacking: " + attackTrigger.enabled);
		}

		if (attacking) {// This here is a cooldown manager on attacking
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}

		//weaponAnimator.SetBool ("Swing", attacking);

	}
}
